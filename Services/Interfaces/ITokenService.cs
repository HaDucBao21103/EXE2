using BusinessObject;
using System.Security.Claims;
using ViewModels;

namespace Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenViewModel> GenerateTokens(Users users);
        ClaimsPrincipal? GetPrincipalFromToken(string token);
        string? GetUserIdFromPrincipal(ClaimsPrincipal claim);
        Task<RefreshToken?> GetRefreshTokenOfUser(string refreshToken, string userId);
        Task<TokenViewModel?> RefreshToken(RefreshToken refreshToken, string userId);
    }
}
