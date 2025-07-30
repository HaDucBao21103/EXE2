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
    public class RecycleLocationsController : ControllerBase
    {
        private readonly IRecycleLocationService _recycleLocationService;
        private readonly ILogger<RecycleLocationsController> _logger;

        public RecycleLocationsController(IRecycleLocationService recycleLocationService, ILogger<RecycleLocationsController> logger)
            => (_recycleLocationService, _logger) = (recycleLocationService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả địa điểm tái chế")]
        public async Task<BaseResponse<IEnumerable<RecycleLocationsResponse>>> Get()
        {
            var result = await _recycleLocationService.GetAllAsync();
            return BaseResponse<IEnumerable<RecycleLocationsResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy địa điểm tái chế theo ID")]
        public async Task<BaseResponse<RecycleLocationsResponse>> Get(string id)
        {
            var result = await _recycleLocationService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<RecycleLocationsResponse>.OkResponse(result)
                : BaseResponse<RecycleLocationsResponse>.NotFoundResponse("Không tìm thấy địa điểm tái chế");
        }

        [HttpGet("getByWastesId/{id}")]
        [SwaggerOperation(Summary = "Tìm kiếm địa điểm tái chế theo loại rác")]
        public async Task<BaseResponse<IEnumerable<RecycleLocationsResponse>>> GetByWastesId(string id)
        {
            var result = await _recycleLocationService.GetByWastesTypeId(id);
            return result.Any()
                ? BaseResponse<IEnumerable<RecycleLocationsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<RecycleLocationsResponse>>.NotFoundResponse("Không tìm thấy địa điểm nào với từ khoá: " + id);
        }

        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm địa điểm tái chế theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<RecycleLocationsResponse>>> Search(string keyword)
        {
            var result = await _recycleLocationService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<RecycleLocationsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<RecycleLocationsResponse>>.NotFoundResponse("Không tìm thấy địa điểm nào với từ khoá: " + keyword);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        [SwaggerOperation(Summary = "Lấy địa điểm có phân trang")]
        public async Task<BaseResponse<IEnumerable<RecycleLocationsResponse>>> GetPaginatedRecycleLocations(int pageNumber, int pageSize)
        {
            var result = await _recycleLocationService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<IEnumerable<RecycleLocationsResponse>>.OkResponse(result.Items)
                : BaseResponse<IEnumerable<RecycleLocationsResponse>>.NotFoundResponse("Không tìm thấy địa điểm tái chế nào");
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Tạo mới địa điểm tái chế")]
        public async Task<BaseResponse<string>> Post([FromBody] RecycleLocationsRequest request)
        {
            try
            {
                var result = await _recycleLocationService.CreateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Tạo địa điểm thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo địa điểm thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Cập nhật địa điểm tái chế")]
        public async Task<BaseResponse<string>> Put([FromBody] RecycleLocationsRequest request)
        {
            try
            {
                var result = await _recycleLocationService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật địa điểm thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật địa điểm thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xoá địa điểm tái chế theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _recycleLocationService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá địa điểm thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá địa điểm thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
