using ViewModels;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<bool> SendConfirmEmailAsync(string email);
        Task<bool> ConfirmEmailAsync(string userId, string token);
        Task<bool> ForgetPasswordAsync(string email);
        Task<bool> ResetPassword(ResetPasswordViewModel request);
        Task<bool> LogoutAsync(TokenViewModel token);
    }
}
