using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using ViewModels;
using ViewModels.Momo;
using ViewModels.Request;

namespace EXE201_SU25.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]
    public class MomoController : ControllerBase
    {
        private readonly IMomoService _momoService;

        public MomoController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost("create-payment")]
        [SwaggerOperation(Summary = "Tạo giao dịch thanh toán Momo", Description = @"
            Sample request:
            {
              ""amount"": 10000,
              ""description"": ""Test"",
              ""userId"": ""11111111-1111-1111-1111-111111111111"",
              ""transactionTypeId"": ""11111111-0000-0000-0000-00000002"",
              ""campaignId"": ""11111111-0000-0000-0000-00000002""
            }
        ")]

        public async Task<IActionResult> CreatePayment([FromBody] TransactionCreateRequest request)
        {
            try
            {
                var transactionInfo = new TransactionInfoModel
                {
                    Amount = request.Amount,
                    Description = request.Description,
                    UserId = request.UserId,
                    TransactionTypeId = request.TransactionTypeId,
                    CampaignId = request.CampaignId.ToString()
                };

                var response = await _momoService.CreatePaymentAsync(transactionInfo);
                if (response == null || string.IsNullOrWhiteSpace(response.PayUrl))
                {
                    return BadRequest("Không thể tạo liên kết thanh toán Momo");
                }

                return Ok(new
                {
                    message = "Tạo liên kết thanh toán thành công",
                    redirectUrl = response.PayUrl
                });
            }
            catch (Exception ex)
            {
                // 🚨 Log lỗi chi tiết ra response để debug
                return BadRequest($"Không thể tạo liên kết thanh toán Momo: {ex.Message}");
            }
        }

        /// <summary>
        /// Nhận phản hồi từ Momo (redirect callback).
        /// </summary>
        /// <param name="collection">Query string từ Momo trả về</param>
        /// <returns>Thông tin trạng thái thanh toán</returns>
        [HttpPost("payment-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymentCallback([FromBody] MomoExecuteResponseModel response)
        {
            //var momoResponse = _momoService.PaymentExecuteAsync(Request.Query);
            if (response.ResultCode == 0) // ✅ Thành công
            {
                await _momoService.HandleSuccessfulPaymentAsync(response);
                return Ok(new
                {
                    message = "Thanh toán thành công",
                    data = response
                });
            }
            else // ❌ Thất bại
            {
                await _momoService.HandleFailedPaymentAsync(response);
                return BadRequest(new
                {
                    message = $"Thanh toán thất bại: {response.Message}",
                    resultCode = response.ResultCode,
                    data = response
                });
            }
        }

    }
}
