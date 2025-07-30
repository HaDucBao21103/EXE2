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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandsController> _logger;
        public BrandsController(IBrandService brandService, ILogger<BrandsController> logger)
            => (_brandService, _logger) = (brandService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả Brands")]
        public async Task<BaseResponse<IEnumerable<BrandsResponse>>> Get()
        {
            var result = await _brandService.GetAllAsync();
            return BaseResponse<IEnumerable<BrandsResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy brand theo ID")]

        public async Task<BaseResponse<BrandsResponse>> Get(string id)
        {
            var result = await _brandService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<BrandsResponse>.OkResponse(result)
                : BaseResponse<BrandsResponse>.NotFoundResponse("Không tìm thấy brand");
        }

        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm brand theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<BrandsResponse>>> Search(string keyword)
        {
            var result = await _brandService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<BrandsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<BrandsResponse>>.NotFoundResponse("Không tìm thấy brand nào với từ khoá: " + keyword);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        [SwaggerOperation(Summary = "Lấy Brand có phân trang")]
        public async Task<BaseResponse<IEnumerable<BrandsResponse>>> GetPaginatedBrands(int pageNumber, int pageSize)
        {
            var result = await _brandService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<IEnumerable<BrandsResponse>>.OkResponse(result.Items)
                : BaseResponse<IEnumerable<BrandsResponse>>.NotFoundResponse("Không tìm thấy brand nào");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Tạo mới brand")]
        public async Task<BaseResponse<string>> Post([FromBody] BrandsRequest request)
        {
            try
            {
                var result = await _brandService.CreateAsync(request);
                return result ? BaseResponse<string>.OkResponse("Tạo brand thành công") : BaseResponse<string>.ErrorResponse("Tạo brand thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Cập nhật brand")]
        public async Task<BaseResponse<string>> Put([FromBody] BrandsRequest request)
        {
            try
            {
                var result = await _brandService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật brand thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật brand thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xoá brand theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _brandService.DeleteAsync(id.ToString());
                return result
                    ? BaseResponse<string>.OkResponse("Xoá brand thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá brand thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi Server: " + ex.Message);
            }
        }
    }
}
