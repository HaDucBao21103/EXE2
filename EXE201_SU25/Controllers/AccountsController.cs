using Core.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using ViewModels;
using ViewModels.Request;
using ViewModels.Response;

namespace EXE201_SU25.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAuthenticateService authenticateService, ITokenService tokenService, ILogger<AccountsController> logger)
            => (_authenticateService, _tokenService, _logger) = (authenticateService, tokenService, logger);

        [AllowAnonymous]
        [SwaggerOperation(Summary = "Đăng nhập hệ thống",
            Description = @"Login bằng tài khoản người dùng.
            
            User: {""username"": ""hoangminh"",""password"": ""123@123""}

            Admin: {""username"": ""linhanh"",""password"": ""123@123""}

            Staff: {""username"": ""ducthien"",""password"": ""123@123""}
            ")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về access token với refresh token và account data")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dữ liệu sai")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Lỗi server")]
        [HttpPost("login")]
        public BaseResponse<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var result = _authenticateService.LoginAsync(request).Result;
                return result != null
                    ? BaseResponse<LoginResponse>.OkResponse(result)
                    : BaseResponse<LoginResponse>.ErrorResponse("Đăng nhập thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<LoginResponse>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        [SwaggerOperation(Summary = "Đăng xuất hệ thống",
            Description = @"Đăng xuất bằng refresh token.

            {
                'accessToken': ''
                'refreshToken': ''
            }")]
        public BaseResponse<string> LogoutAsync([FromBody] TokenViewModel tokenRequest)
        {
            try
            {
                var result = _authenticateService.LogoutAsync(tokenRequest).Result;
                return result
                     ? BaseResponse<string>.OkResponse("Đăng xuất thành công")
                     : BaseResponse<string>.ErrorResponse("Đăng xuất thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [SwaggerOperation(Summary = "Làm mới token",
            Description = @"Làm mới access token và refresh token bằng refresh token hiện tại.
            Sample request:
            {
               ""accessToken"": """"
               ""refreshToken"": """"
            }")]
        public BaseResponse<TokenViewModel> RefreshToken([FromBody] TokenViewModel request)
        {
            try
            {
                var principal = _tokenService.GetPrincipalFromToken(request.AccessToken);
                if (principal == null)
                    return BaseResponse<TokenViewModel>.ErrorResponse("Token không hợp lệ");

                var userId = principal.Claims.First(x => x.Type == "uid").Value;

                var storedToken = _tokenService.GetRefreshTokenOfUser(request.RefreshToken, userId).Result;

                if (storedToken == null || storedToken.Used || storedToken.Revoked || storedToken.ExpiryDate < DateTime.UtcNow)
                    return BaseResponse<TokenViewModel>.ErrorResponse("Refresh token không hợp lệ");

                storedToken.Used = true;
                var result = _tokenService.RefreshToken(storedToken, userId).Result;
                return result != null
                     ? BaseResponse<TokenViewModel>.OkResponse(result)
                     : BaseResponse<TokenViewModel>.ErrorResponse("Lỗi khi làm mới token");
            }
            catch (BaseException.CoreException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<TokenViewModel>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPost("confirm-email")]
        [SwaggerOperation(Summary = "Xác thực email",
            Description = @"Xác thực email bằng userId và token.
            Sample request:
            {
                'userId': '11111111-1111-1111-111111111111',
                'token': '...'
            }")]
        public BaseResponse<string> ConfirmEmail(string userId, string token)
        {
            try
            {
                var result = _authenticateService.ConfirmEmailAsync(userId, token).Result;
                return result
                    ? BaseResponse<string>.OkResponse("Xác thực email thành công")
                    : BaseResponse<string>.ErrorResponse("Xác thực email thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        [SwaggerOperation(Summary = "Quên mật khẩu",
            Description = @"Gửi yêu cầu đặt lại mật khẩu đến email đã đăng ký.
            Sample request:
            {
                'email': ''
            }")]
        public BaseResponse<string> ForgetPassword(string email)
        {
            try
            {
                var result = _authenticateService.ForgetPasswordAsync(email).Result;
                return result
                     ? BaseResponse<string>.OkResponse("Yêu cầu đặt lại mật khẩu đã được gửi đến email của bạn")
                     : BaseResponse<string>.ErrorResponse("Không tìm thấy tài khoản với email này");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(Summary = "Đặt lại mật khẩu",
            Description = @"Đặt lại mật khẩu bằng mã xác thực đã gửi đến email.
            Sample request:
            {
                'email': '',
                'token': '',
                'newPassword': ''
            }")]
        public BaseResponse<string> ResetPassword(ResetPasswordViewModel request)
        {
            try
            {
                var result = _authenticateService.ResetPassword(request).Result;
                return result
                    ? BaseResponse<string>.OkResponse("Đặt lại mật khẩu thành công")
                    : BaseResponse<string>.ErrorResponse("Đặt lại mật khẩu thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
