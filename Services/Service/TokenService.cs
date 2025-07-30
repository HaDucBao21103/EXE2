
using BusinessObject;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ViewModels;

namespace Services.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<TokenService> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<TokenViewModel> GenerateTokens(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:Key"]);
            var jwtId = Guid.NewGuid().ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Authentication:Jwt:Issuer"],
                Audience = _configuration["Authentication:Jwt:Audience"],
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, jwtId),
                    new Claim("uid", user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Roles?.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Authentication:Jwt:Lifetime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken
            {
                JwtId = jwtId,
                UserId = user.Id,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(double.Parse(_configuration["Authentication:Jwt:RefreshTokenTTL"]))
            };

            await _unitOfWork.GetRepository<RefreshToken>().CreateAsync(refreshToken);
            await _unitOfWork.SaveAsync();
            return new TokenViewModel
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken.Token
            };
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = _configuration["Authentication:Jwt:Audience"],
                    ValidIssuer = _configuration["Authentication:Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:Key"])),
                }, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }
            }
            catch { }

            return null;
        }

        public string? GetUserIdFromPrincipal(ClaimsPrincipal claim)
        {
            try
            {
                return claim.FindFirst("uid")?.Value.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<RefreshToken?> GetRefreshTokenOfUser(string refreshToken, string userId)
        {
            var result = await _unitOfWork.GetRepository<RefreshToken>().GetOneAsync(r => r.Token == refreshToken && r.UserId == Guid.Parse(userId));
            return result;
        }

        public async Task<TokenViewModel?> RefreshToken(RefreshToken refreshToken, string userId)
        {
            await _unitOfWork.GetRepository<RefreshToken>().UpdateAsync(refreshToken);
            await _unitOfWork.SaveAsync();

            var user = await _unitOfWork.GetRepository<Users>().GetOneAsync(u => u.Id == Guid.Parse(userId) && u.IsDeleted == false);
            if (user == null)
            {
                return null;
            }

            var generateToken = await GenerateTokens(user);
            return generateToken;
        }
    }
}
