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
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostService postService, ILogger<PostsController> logger)
            => (_postService, _logger) = (postService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả Posts")]
        public async Task<BaseResponse<IEnumerable<PostsResponse>>> Get()
        {
            var result = await _postService.GetAllAsync();
            return BaseResponse<IEnumerable<PostsResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy post theo ID")]
        public async Task<BaseResponse<PostsResponse>> Get(string id)
        {
            var result = await _postService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<PostsResponse>.OkResponse(result)
                : BaseResponse<PostsResponse>.NotFoundResponse("Không tìm thấy post");
        }


        [HttpGet("search")]
        [SwaggerOperation(Summary = "Tìm kiếm post theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<PostsResponse>>> Search(string? keyword)
        {
            var result = await _postService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<PostsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<PostsResponse>>.NotFoundResponse($"Không tìm thấy post nào với từ khoá: {keyword}");
        }

        [HttpGet("paging")]
        [SwaggerOperation(Summary = "Lấy post có phân trang")]
        public async Task<BaseResponse<BasePaginatedList<PostsResponse>>> GetPaginatedPosts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await _postService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<BasePaginatedList<PostsResponse>>.OkResponse(result)
                : BaseResponse<BasePaginatedList<PostsResponse>>.NotFoundResponse("Không tìm thấy post nào");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Tạo mới post")]
        public async Task<BaseResponse<string>> Post([FromBody] PostsCreateRequest request)
        {
            try
            {
                var result = await _postService.CreateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Tạo post thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo post thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Cập nhật post")]
        public async Task<BaseResponse<string>> Put([FromBody] PostsRequest request)
        {
            try
            {
                var result = await _postService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật post thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật post thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("update_status")]
        [SwaggerOperation(Summary = "Update status")]
        public async Task<BaseResponse<string>> PutStatus([FromQuery] string status, [FromQuery] string id)
        {
            try
            {
                var result = await _postService.UpdateStatusAsync(status,id);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật status post thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật status post thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xoá post theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _postService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá post thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá post thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi Server: " + ex.Message);
            }
        }


    }
}
