using BusinessObject;
using Core.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace EXE201_SU25.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        public readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả Bookings")]
        public async Task<BaseResponse<IEnumerable<Bookings>>> Get()
        {
            var result = await _bookingService.GetAllAsync();
            return BaseResponse<IEnumerable<Bookings>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy booking theo ID")]
        public async Task<BaseResponse<Bookings>> Get(string id)
        {
            var result = await _bookingService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<Bookings>.OkResponse(result)
                : BaseResponse<Bookings>.NotFoundResponse("Không tìm thấy booking");
        }

        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm booking theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<Bookings>>> Search(string keyword)
        {
            var result = await _bookingService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<Bookings>>.OkResponse(result)
                : BaseResponse<IEnumerable<Bookings>>.NotFoundResponse("Không tìm thấy booking nào với từ khoá: " + keyword);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        [SwaggerOperation(Summary = "Lấy booking có phân trang")]
        public async Task<BaseResponse<BasePaginatedList<Bookings>>> GetPaginatedBlogs(int pageNumber, int pageSize)
        {
            var result = await _bookingService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<BasePaginatedList<Bookings>>.OkResponse(result)
                : BaseResponse<BasePaginatedList<Bookings>>.NotFoundResponse("Không tìm thấy booking nào");
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Tạo mới booking",
          Description = @"
            Sample Request
            {
            ""location"": ""Trường Tiểu Học ABC, Quận 10"",
            ""bookingDate"": ""2025-07-20T09:00:00Z"",
            ""description"": ""Buổi tuyên truyền về phân loại rác thải và tái chế cho học sinh khối 3 và khối 4. Dự kiến khoảng 100 học sinh tham gia."",
            ""contactPersonName"": ""Cô Nguyễn Thị Lan"",
            ""contactPersonPhone"": ""0912345678"",
            ""contactPersonEmail"": ""lan.nguyen@example.edu.vn"",
            ""status"": ""Pending"",
            ""userId"": ""11111111-1111-1111-1111-111111111111"", 
            ""createdBy"": ""system"",
            ""lastUpdatedBy"": ""system"",
            ""deletedBy"": null,
            ""createdTime"": ""2025-06-25T14:30:00Z"",
            ""lastUpdatedTime"": ""2025-06-25T14:30:00Z"",
            ""deletedTime"": null,
            ""isDeleted"": false
            }")]
        public async Task<BaseResponse<string>> Post([FromBody] Bookings request)
        {
            try
            {
                var result = await _bookingService.CreateAsync(request);
                return result ? BaseResponse<string>.OkResponse("Tạo booking thành công") : BaseResponse<string>.ErrorResponse("Tạo booking thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Cập nhật booking",
             Description = @"
            Sample Request
               {
                ""id"": ""41a1d91484d44ccb8dcaacba464db3ad"",
                ""location"": ""Trường Tiểu Học ABCD, Quận 10"",
                ""bookingDate"": ""2025-07-20T09:00:00Z"",
                ""description"": ""Buổi tuyên truyền về phân loại rác thải và tái chế cho học sinh khối 3 và khối 4. Dự kiến khoảng 100 học sinh tham gia."",
                ""contactPersonName"": ""Cô Nguyễn Thị Lan"",
                ""contactPersonPhone"": ""0912345678"",
                ""contactPersonEmail"": ""lan.nguyen@example.edu.vn"",
                ""status"": ""Pending"",
                ""userId"": ""11111111-1111-1111-1111-111111111111"", 
                ""createdBy"": ""system"",
                ""lastUpdatedBy"": ""system"",
                ""deletedBy"": null,
                ""createdTime"": ""2025-06-25T14:30:00Z"",
                ""lastUpdatedTime"": ""2025-06-25T14:30:00Z"",
                ""deletedTime"": null,
                ""isDeleted"": false
        }")]
        public async Task<BaseResponse<string>> Put([FromBody] Bookings request)
        {
            try
            {
                var result = await _bookingService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật booking thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật booking thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Xoá booking theo ID")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _bookingService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá booking thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá booking thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi Server: " + ex.Message);
            }
        }
    }
}
