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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
            => (_commentService, _logger) = (commentService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả Comments")]
        public async Task<BaseResponse<IEnumerable<CommentsResponse>>> Get()
        {
            var result = await _commentService.GetAllAsync();
            return BaseResponse<IEnumerable<CommentsResponse>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy comment theo ID")]
        public async Task<BaseResponse<CommentsResponse>> Get(string id)
        {
            var result = await _commentService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<CommentsResponse>.OkResponse(result)
                : BaseResponse<CommentsResponse>.NotFoundResponse("Không tìm thấy comment");
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Tìm kiếm comment theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<CommentsResponse>>> Search([FromQuery] string? keyword)
        {
            var result = await _commentService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<CommentsResponse>>.OkResponse(result)
                : BaseResponse<IEnumerable<CommentsResponse>>.NotFoundResponse("Không tìm thấy comment nào với từ khoá: " + keyword);
        }

        [HttpGet("paging")]
        [SwaggerOperation(Summary = "Lấy Comment có phân trang")]
        public async Task<BaseResponse<BasePaginatedList<CommentsResponse>>> GetPaginatedComments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = await _commentService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<BasePaginatedList<CommentsResponse>>.OkResponse(result)
                : BaseResponse<BasePaginatedList<CommentsResponse>>.NotFoundResponse("Không tìm thấy comment nào");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff,User")]
        [SwaggerOperation(Summary = "Tạo mới comment")]
        public async Task<BaseResponse<string>> Post([FromBody] CommentsRequest request)
        {
            try
            {
                var result = await _commentService.CreateAsync(request);
                return result ? BaseResponse<string>.OkResponse("Tạo comment thành công") : BaseResponse<string>.ErrorResponse("Tạo comment thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Staff,User")]
        [SwaggerOperation(Summary = "Cập nhật comment")]
        public async Task<BaseResponse<string>> Put([FromBody] CommentsRequest request)
        {
            try
            {
                var result = await _commentService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật comment thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật comment thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff,User")]
        [SwaggerOperation(Summary = "Xoá comment theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _commentService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá comment thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá comment thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi Server: " + ex.Message);
            }
        }
    }
}
