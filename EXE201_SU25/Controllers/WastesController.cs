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
    public class WastesController : ControllerBase
    {
        private readonly IWasteService _wasteService;
        private readonly ILogger<WastesController> _logger;

        public WastesController(IWasteService wasteService, ILogger<WastesController> logger)
            => (_wasteService, _logger) = (wasteService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả rác thải")]
        public async Task<BaseResponse<IEnumerable<WasteResponse>>> Get()
        {
            var result = await _wasteService.GetAllAsync();
            return BaseResponse<IEnumerable<WasteResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy rác thải theo ID")]
        public async Task<BaseResponse<WasteResponse>> Get(string id)
        {
            var result = await _wasteService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<WasteResponse>.OkResponse(result)
                : BaseResponse<WasteResponse>.NotFoundResponse("Không tìm thấy rác thải");
        }
        [HttpGet("GetWastesByWastesTypeId/{id}")]
        [SwaggerOperation(Summary = "Lấy rác thải theo WastesTypeID")]
        public async Task<BaseResponse<IEnumerable<WasteResponse>>> GetWastesByWastesTypeId(string id)
        {
            var result = await _wasteService.GetWastesByWastesTypeId(id);
            return result != null
                ? BaseResponse<IEnumerable<WasteResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<WasteResponse>>.NotFoundResponse("Không tìm thấy rác thải");
        }
        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm rác thải theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<WasteResponse>>> Search(string keyword)
        {
            var result = await _wasteService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<WasteResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<WasteResponse>>.NotFoundResponse("Không tìm thấy rác thải nào với từ khoá: " + keyword);
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        [SwaggerOperation(Summary = "Lấy rác thải có phân trang")]
        public async Task<BaseResponse<IEnumerable<WasteResponse>>> GetPaginatedWastes(int pageNumber, int pageSize)
        {
            var result = await _wasteService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<IEnumerable<WasteResponse>>.OkResponse(result.Items)
                : BaseResponse<IEnumerable<WasteResponse>>.NotFoundResponse("Không tìm thấy rác thải nào");
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Thêm rác thải mới")]
        public async Task<BaseResponse<string>> Post([FromBody] WasteRequest request)
        {
            try
            {
                var result = await _wasteService.CreateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Thêm rác thải thành công")
                    : BaseResponse<string>.ErrorResponse("Thêm rác thải thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Staff")]
        [SwaggerOperation(Summary = "Cập nhật rác thải")]
        public async Task<BaseResponse<string>> Put([FromBody] WasteRequest request)
        {
            try
            {
                var result = await _wasteService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật rác thải thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật rác thải thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xóa rác thải theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _wasteService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xóa rác thải thành công")
                    : BaseResponse<string>.ErrorResponse("Xóa rác thải thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
