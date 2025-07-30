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
    public class WasteTypesController : ControllerBase
    {
        private readonly IWasteTypeService _wasteTypeService;
        private readonly ILogger<WasteTypesController> _logger;

        public WasteTypesController(IWasteTypeService wasteTypeService, ILogger<WasteTypesController> logger)
            => (_wasteTypeService, _logger) = (wasteTypeService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả loại rác thải")]
        public async Task<BaseResponse<IEnumerable<WasteTypesResponse>>> Get()
        {
            var result = await _wasteTypeService.GetAllAsync();
            return BaseResponse<IEnumerable<WasteTypesResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy loại rác thải theo ID")]
        public async Task<BaseResponse<WasteTypesResponse>> Get(string id)
        {
            var result = await _wasteTypeService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<WasteTypesResponse>.OkResponse(result)
                : BaseResponse<WasteTypesResponse>.NotFoundResponse("Không tìm thấy loại rác thải");
        }

        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm loại rác thải theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<WasteTypesResponse>>> Search(string keyword)
        {
            var result = await _wasteTypeService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<WasteTypesResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<WasteTypesResponse>>.NotFoundResponse($"Không tìm thấy loại rác thải nào với từ khoá: {keyword}");
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        [SwaggerOperation(Summary = "Lấy loại rác thải có phân trang")]
        public async Task<BaseResponse<IEnumerable<WasteTypesResponse>>> GetPaginated(int pageNumber, int pageSize)
        {
            var result = await _wasteTypeService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<IEnumerable<WasteTypesResponse>>.OkResponse(result.Items)
                : BaseResponse<IEnumerable<WasteTypesResponse>>.NotFoundResponse("Không tìm thấy loại rác thải nào");
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Tạo mới loại rác thải")]
        public async Task<BaseResponse<string>> Post([FromBody] WasteTypesRequest request)
        {
            try
            {
                var result = await _wasteTypeService.CreateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Tạo mới thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo mới thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo WasteType");
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Cập nhật loại rác thải")]
        public async Task<BaseResponse<string>> Put([FromBody] WasteTypesRequest request)
        {
            try
            {
                var result = await _wasteTypeService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật WasteType");
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xoá loại rác thải theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _wasteTypeService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, "Lỗi khi xoá WasteType");
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
