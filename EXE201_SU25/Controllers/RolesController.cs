using Core.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using ViewModels.Request;
using ViewModels.Response;

namespace EXE201_SU25.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService roleService, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả Roles")]
        public async Task<BaseResponse<IEnumerable<RolesResponse>>> Get()
        {
            var result = await _roleService.GetAllAsync();
            return BaseResponse<IEnumerable<RolesResponse>>.OkResponse(result);
        }

        /// <summary>
        /// Retrieves a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy Role theo ID")]
        public async Task<BaseResponse<RolesResponse>> Get(string id)
        {
            var result = await _roleService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<RolesResponse>.OkResponse(result)
                : BaseResponse<RolesResponse>.NotFoundResponse("Không tìm thấy role");
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="request">The request body for creating a role.</param>
        [HttpPost]
        [SwaggerOperation(Summary = "Tạo mới role")]
        public async Task<BaseResponse<string>> Post([FromBody] RolesRequest request)
        {
            try
            {
                var result = await _roleService.CreateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Tạo role thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo role thất bại");
            }
            catch (BaseException.ErrorException ex) // Assuming BaseException.ErrorException is your custom exception
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="request">The request body for updating a role.</param>
        [HttpPut]
        [SwaggerOperation(Summary = "Cập nhật role")]
        public async Task<BaseResponse<string>> Put([FromBody] RolesRequest request, string id)
        {
            try
            {
                var result = await _roleService.UpdateAsync(id, request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật role thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật role thất bại");
            }
            catch (BaseException.ErrorException ex) // Assuming BaseException.ErrorException is your custom exception
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xoá role theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _roleService.DeleteAsync(id); // Ensure id is passed directly, no ToString() needed if it's already string
                return result
                    ? BaseResponse<string>.OkResponse("Xoá role thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá role thất bại");
            }
            catch (BaseException.ErrorException ex) // Catch specific error exception for better handling
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi Server: " + ex.Message);
            }
        }
    }
}
