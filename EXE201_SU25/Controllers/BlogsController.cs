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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<BlogsController> _logger;
        public BlogsController(IBlogService blogService, ILogger<BlogsController> logger)
            => (_blogService, _logger) = (blogService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả Blogs")]
        public async Task<BaseResponse<IEnumerable<BlogsResponse>>> Get()
        {
            var result = await _blogService.GetAllAsync();
            return BaseResponse<IEnumerable<BlogsResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy blog theo ID")]
        public async Task<BaseResponse<BlogsResponse>> Get(string id)
        {
            var result = await _blogService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<BlogsResponse>.OkResponse(result)
                : BaseResponse<BlogsResponse>.NotFoundResponse("Không tìm thấy blog");
        }
        [HttpGet("getByBlogTypeId/{blogTypeId}")]
        [SwaggerOperation(Summary = "Lấy blog theo BlogType ID")]

        public async Task<BaseResponse<IEnumerable<BlogsResponse>>> GetByBlogType(string blogTypeId)
        {
            var result = await _blogService.GetByBlogTypeIdAsync(blogTypeId);
            return result != null
                ? BaseResponse<IEnumerable<BlogsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<BlogsResponse>>.NotFoundResponse("Không tìm thấy blog");
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Tìm kiếm blog theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<BlogsResponse>>> Search(string? keyword)
        {
            var result = await _blogService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<BlogsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<BlogsResponse>>.NotFoundResponse("Không tìm thấy blog nào với từ khoá: " + keyword);
        }

        [HttpGet("paging")]
        [SwaggerOperation(Summary = "Lấy Blog có phân trang")]
        public async Task<BaseResponse<BasePaginatedList<BlogsResponse>>> GetPaginatedBlogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await _blogService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<BasePaginatedList<BlogsResponse>>.OkResponse(result)
                : BaseResponse<BasePaginatedList<BlogsResponse>>.NotFoundResponse("Không tìm thấy blog nào");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Tạo mới blog",
            Description = @"
            Sample Reuest
            {
              ""createdBy"": ""string"",
              ""lastUpdatedBy"": ""string"",
              ""deletedBy"": ""string"",
              ""createdTime"": ""2025-06-14T03:39:12.441Z"",
              ""lastUpdatedTime"": ""2025-06-14T03:39:12.441Z"",
              ""deletedTime"": ""2025-06-14T03:39:12.441Z"",
              ""isDeleted"": false,
              ""title"": ""string"",
              ""content"": ""string"",
              ""imageUrl"": ""string"",
              ""authorId"": ""33333333-3333-3333-3333-333333333333"",
              ""blogTypeId"": ""55555555-5555-5555-5555-555555555555"",
              ""status"": ""string""
            }")]
        public async Task<BaseResponse<string>> Post([FromBody] BlogsRequest request)
        {
            try
            {
                var result = await _blogService.CreateAsync(request);
                return result ? BaseResponse<string>.OkResponse("Tạo blog thành công") : BaseResponse<string>.ErrorResponse("Tạo blog thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Cập nhật blog")]
        public async Task<BaseResponse<string>> Put([FromBody] BlogsRequest request)
        {
            try
            {
                var result = await _blogService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật blog thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật blog thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Xoá blog theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _blogService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá blog thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá blog thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi Server: " + ex.Message);
            }
        }
    }
}
