using BusinessObject;
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
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignsService _campaignService;
        private readonly ILogger<CampaignsController> _logger;

        public CampaignsController(ICampaignsService campaignService, ILogger<CampaignsController> logger)
            => (_campaignService, _logger) = (campaignService, logger);

        [HttpGet]
        [SwaggerOperation(Summary = "Lấy tất cả campaigns")]
        public async Task<BaseResponse<IEnumerable<Campaigns>>> Get()
        {
            var result = await _campaignService.GetAllAsync();
            return BaseResponse<IEnumerable<Campaigns>>.OkResponse(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Lấy campaign theo ID")]
        public async Task<BaseResponse<Campaigns>> Get(string id)
        {
            var result = await _campaignService.GetByIdAsync(id);
            return result != null
                ? BaseResponse<Campaigns>.OkResponse(result)
                : BaseResponse<Campaigns>.NotFoundResponse("Không tìm thấy campaign");
        }

        [HttpGet("summary")]
        [SwaggerOperation(Summary = "Tính toán sum transaction của campaign")]

        public async Task<BaseResponse<List<CampaignTransactionSummaryResponse>>> GetCampaignTransactionSummaryAsync()
        {
            try
            {
                var result = await _campaignService.GetCampaignTransactionSummaryAsync();
                return BaseResponse<List<CampaignTransactionSummaryResponse>>.OkResponse(result);
            }
            catch (Exception ex) { }
            return BaseResponse<List<CampaignTransactionSummaryResponse>>.NotFoundResponse("Không tìm thấy campaign nào");
        }

        [HttpGet("summary/{id}")]
        [SwaggerOperation(Summary = "Tính toán sum transaction của campaign")]

        public async Task<BaseResponse<CampaignTransactionSummaryResponse>> GetCampaignTransactionSummaryByIdAsync(string id)
        {
            try
            {
                var result = await _campaignService.GetCampaignTransactionSummaryByIdAsync(id);
                return BaseResponse<CampaignTransactionSummaryResponse>.OkResponse(result);
            }
            catch (Exception ex) { }
            return BaseResponse<CampaignTransactionSummaryResponse>.NotFoundResponse("Không tìm thấy campaign nào");
        }

        [HttpGet("search/{keyword}")]
        [SwaggerOperation(Summary = "Tìm kiếm campaign theo từ khoá")]
        public async Task<BaseResponse<IEnumerable<Campaigns>>> Search(string keyword)
        {
            var result = await _campaignService.SearchAsync(keyword);
            return result.Any()
                ? BaseResponse<IEnumerable<Campaigns>>.OkResponse(result)
                : BaseResponse<IEnumerable<Campaigns>>.NotFoundResponse($"Không tìm thấy campaign nào với từ khoá: {keyword}");
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        [SwaggerOperation(Summary = "Lấy campaign có phân trang")]
        public async Task<BaseResponse<IEnumerable<Campaigns>>> GetPaginated(int pageNumber, int pageSize)
        {
            var result = await _campaignService.GetPaginatedListAsync(pageNumber, pageSize);
            return result.Items.Any()
                ? BaseResponse<IEnumerable<Campaigns>>.OkResponse(result.Items)
                : BaseResponse<IEnumerable<Campaigns>>.NotFoundResponse("Không tìm thấy campaign nào");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Tạo mới campaign")]
        public async Task<BaseResponse<string>> Post([FromBody] CreateCampaignsRequest request)
        {
            try
            {
                var campaign = new Campaigns
                {
                    Title = request.Title,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    UserId = request.UserId,
                    TargetAmount = request.TargetAmount,
                    RaisedAmount = request.RaisedAmount,
                    Status = request.Status,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                };

                var result = await _campaignService.CreateAsync(campaign);
                return result
                    ? BaseResponse<string>.OkResponse("Tạo campaign thành công")
                    : BaseResponse<string>.ErrorResponse("Tạo campaign thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Cập nhật campaign")]
        public async Task<BaseResponse<string>> Put([FromBody] Campaigns request)
        {
            try
            {
                var result = await _campaignService.UpdateAsync(request);
                return result
                    ? BaseResponse<string>.OkResponse("Cập nhật campaign thành công")
                    : BaseResponse<string>.ErrorResponse("Cập nhật campaign thất bại");
            }
            catch (BaseException.ErrorException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        [SwaggerOperation(Summary = "Xoá campaign theo ID (soft delete)")]
        public async Task<BaseResponse<string>> Delete(string id)
        {
            try
            {
                var result = await _campaignService.DeleteAsync(id);
                return result
                    ? BaseResponse<string>.OkResponse("Xoá campaign thành công")
                    : BaseResponse<string>.ErrorResponse("Xoá campaign thất bại");
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BaseResponse<string>.ErrorResponse("Lỗi server: " + ex.Message);
            }
        }
    }
}
