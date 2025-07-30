using Core.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using ViewModels.Request;
using ViewModels.Response;

namespace EXE201_SU25.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
            => (_logger, _userService) = (logger, userService);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả người dùng")]
        public async Task<BaseResponse<IEnumerable<UsersResponse>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return BaseResponse<IEnumerable<UsersResponse>>.OkResponse(result);
        }

        [HttpGet("current")]
        [Authorize(Roles = "User")]
        [SwaggerOperation(Summary = "Lấy người dùng hiện tại đang đăng nhập")]
        public async Task<BaseResponse<UsersResponse>> GetCurrentUserAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BaseResponse<UsersResponse>.UnauthorizeResponse("Bạn chưa đăng nhập vui lòng đăng nhập");
            }
            var response = await _userService.GetCurrentUserAsync(User);
            return response != null
                ? BaseResponse<UsersResponse>.OkResponse(response)
                : BaseResponse<UsersResponse>.NotFoundResponse("Không tìm thấy người dùng");
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Đăng ký tài khoản người dùng")]
        public async Task<BaseResponse<string>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BaseResponse<string>.ErrorResponse(string.Join(", ", errors));
                }

                var response = await _userService.RegisterAsync(request);
                return response
                    ? BaseResponse<string>.OkResponse("Tạo tài khoản thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo tài khoản thất bại");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut("update-profile")]
        [Authorize(Roles = "User")]
        [SwaggerOperation(Summary = "Cập nhật thông tin người dùng")]
        public async Task<BaseResponse<string>> UpdateProfile([FromBody] UsersRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BaseResponse<string>.ErrorResponse(string.Join(", ", errors));
                }

                var result = await _userService.UpdateProfileAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật thông tin thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut("update-role")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Cập nhật quyền người dùng")]
        public async Task<BaseResponse<string>> UpdateRole([FromBody] UsersRequestRole request)
        {
            try
            {
                var result = await _userService.UpdateRoleAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật thông tin thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
