using AutoMapper;
using BusinessObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;
        public AuthenticateService(IUnitOfWork unitOfWork, UserManager<Users> userManager, ITokenService tokenService, IConfiguration configuration, IEmailSenderService emailSenderService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _emailSenderService = emailSenderService;
            _mapper = mapper;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var passwordHasher = new PasswordHasher<Users>();
            var user = await _unitOfWork.GetRepository<Users>().GetOneAsync(x => (x.UserName == request.Username || x.Email == request.Username) && x.IsDeleted == false);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return null;
            }

            //if (!user.EmailConfirmed)
            //{
            //    return null;
            //}

            var token = await _tokenService.GenerateTokens(user);

            var userResponse = _mapper.Map<UsersResponse>(user);

            var loginResponse = new LoginResponse
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Users = userResponse,
            };

            return loginResponse;
        }

        public async Task<bool> LogoutAsync(TokenViewModel token)
        {
            var tokens = await _unitOfWork.GetRepository<RefreshToken>().GetOneAsync(rf => rf.Token == token.RefreshToken);
            if (tokens == null)
            {
                return false;
            }
            tokens.Used = true;
            tokens.Revoked = true;
            await _unitOfWork.GetRepository<RefreshToken>().UpdateAsync(tokens);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> SendConfirmEmailAsync(string email)
        {
            var users = await _unitOfWork.GetRepository<Users>().GetOneAsync(x => x.Email == email && x.IsDeleted == false);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(users);
            var confirmationLink = $"{_configuration["Email:EmailResponseURL"]}/confirm-email?userId={users.Id}&token={Uri.EscapeDataString(token)}";
            await _emailSenderService.SendEmailAsync(users.Email, "Xác nhận email", $"Vui lòng xác nhận email của bạn qua đường dẫn sau: {confirmationLink}");
            return true;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            if (userId.IsNullOrEmpty() || token.IsNullOrEmpty())
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            var users = await _unitOfWork.GetRepository<Users>().GetOneAsync(x => x.Email == email && x.IsDeleted == false);
            var token = await _userManager.GeneratePasswordResetTokenAsync(users);
            var resetLink = $"{_configuration["Email:EmailResponseURL"]}/reset-password?userId={users.Id}&token={Uri.EscapeDataString(token)}";
            await _emailSenderService.SendEmailAsync(users.Email, "Đặt lại mật khẩu", $"Vui lòng đặt lại mật khẩu của bạn qua đường dẫn sau: {resetLink}");
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordViewModel request)
        {
            var users = await _unitOfWork.GetRepository<Users>().GetOneAsync(x => x.Id == request.UserId && x.IsDeleted == false);
            if (users == null || !users.EmailConfirmed)
            {
                return false;
            }
            var result = await _userManager.ResetPasswordAsync(users, request.Token, request.NewPassword);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}
