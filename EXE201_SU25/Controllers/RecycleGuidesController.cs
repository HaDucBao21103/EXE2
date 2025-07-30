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
    public class RecycleGuidesController : ControllerBase
    {
        private readonly IRecycleGuideService _recycleGuideService;
        private readonly ILogger<RecycleGuidesController> _logger;

        public RecycleGuidesController(IRecycleGuideService recycleGuideService, ILogger<RecycleGuidesController> logger)
            => (_recycleGuideService, _logger) = (recycleGuideService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả hướng dẫn tái chế")]
        public async Task<BaseResponse<IEnumerable<RecycleGuidesResponse>>> Get()
        {
            var result = await _recycleGuideService.GetAllAsync();
            return BaseResponse<IEnumerable<RecycleGuidesResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy hướng dẫn tái chế theo ID")]
        public async Task<BaseResponse<RecycleGuidesResponse>> Get(string id)
        {
            var result = await _recycleGuideService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<RecycleGuidesResponse>.OkResponse(result)
                : BaseResponse<RecycleGuidesResponse>.NotFoundResponse("Không tìm thấy hướng dẫn tái chế");
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Tìm kiếm hướng dẫn tái chế theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<RecycleGuidesResponse>>> Search(string? keyword)
        {
            var result = await _recycleGuideService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<RecycleGuidesResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<RecycleGuidesResponse>>.NotFoundResponse("Không tìm thấy hướng dẫn với từ khoá: " + keyword);
        }

        [HttpGet("paging")]
        [SwaggerOperation(Summary = "Lấy danh sách hướng dẫn tái chế có phân trang")]
        public async Task<BaseResponse<BasePaginatedList<RecycleGuidesResponse>>> GetPaginatedRecycleGuides([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await _recycleGuideService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<BasePaginatedList<RecycleGuidesResponse>>.OkResponse(result)
                : BaseResponse<BasePaginatedList<RecycleGuidesResponse>>.NotFoundResponse("Không tìm thấy hướng dẫn nào");
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Tạo mới hướng dẫn tái chế")]
        public async Task<BaseResponse<string>> Post([FromBody] RecycleGuidesRequest request)
        {
            try
            {
                var result = await _recycleGuideService.CreateAsync(request);
                return result ? BaseResponse<string>.OkResponse("Tạo hướng dẫn thành công") : BaseResponse<string>.ErrorResponse("Tạo thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Cập nhật hướng dẫn tái chế")]
        public async Task<BaseResponse<string>> Put([FromBody] RecycleGuidesRequest request)
        {
            try
            {
                var result = await _recycleGuideService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xoá hướng dẫn tái chế theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _recycleGuideService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
