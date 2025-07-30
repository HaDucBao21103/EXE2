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
    public class BlogTypesController : ControllerBase
    {
        private readonly IBlogTypeService _blogTypeService;
        private readonly ILogger<BlogTypesController> _logger;
        public BlogTypesController(IBlogTypeService blogTypeService, ILogger<BlogTypesController> logger)
            => (_blogTypeService, _logger) = (blogTypeService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả loại blog")]
        public async Task<BaseResponse<IEnumerable<BlogTypesResponse>>> Get()
        {
            var result = await _blogTypeService.GetAllAsync();
            return BaseResponse<IEnumerable<BlogTypesResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy loại blog theo ID")]
        public async Task<BaseResponse<BlogTypesResponse>> Get(string id)
        {
            var result = await _blogTypeService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<BlogTypesResponse>.OkResponse(result)
                : BaseResponse<BlogTypesResponse>.NotFoundResponse("Không tìm thấy loại blog");
        }

        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm loại blog theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<BlogTypesResponse>>> Search(string keyword)
        {
            var result = await _blogTypeService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<BlogTypesResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<BlogTypesResponse>>.NotFoundResponse("Không tìm thấy loại blog nào với từ khoá: " + keyword);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        [SwaggerOperation(Summary = "Lấy Blog Type có phân trang")]

        public async Task<BaseResponse<IEnumerable<BlogTypesResponse>>> GetPaginatedBlogTypes(int pageNumber, int pageSize)
        {
            var result = await _blogTypeService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<IEnumerable<BlogTypesResponse>>.OkResponse(result.Items)
                : BaseResponse<IEnumerable<BlogTypesResponse>>.NotFoundResponse("Không tìm thấy loại blog nào");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Tạo mới loại blog")]
        public async Task<BaseResponse<string>> Post([FromBody] BlogTypesRequest request)
        {
            try
            {
                var result = await _blogTypeService.CreateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Tạo loại blog thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo loại blog thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Cập nhật loại blog")]
        public async Task<BaseResponse<string>> Put([FromBody] BlogTypesRequest request)
        {
            try
            {
                var result = await _blogTypeService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật loại blog thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật loại blog thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xoá loại blog")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _blogTypeService.DeleteAsync(id.ToString());
                return result
                    ? BaseResponse<string>.OkResponse("Xoá loại blog thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá loại blog thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
