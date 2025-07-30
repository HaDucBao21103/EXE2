using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Text;
using System.Text.Json;
using ViewModels;
using ViewModels.Momo;


namespace Services.Service
{
    public class MomoService : IMomoService
    {
        private readonly MomoOptionModel _config;
        private readonly IUnitOfWork _unitOfWork;

        public MomoService(IOptions<MomoOptionModel> options, IUnitOfWork unitOfWork)
        {
            _config = options.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(TransactionInfoModel model)
        {
            // Tạo orderId và requestId
            var orderId = Guid.NewGuid().ToString();
            var requestId = Guid.NewGuid().ToString();
            var amount = ((int)model.Amount).ToString();

            // Tạo chuỗi raw để ký
            var rawHash = $"partnerCode={_config.PartnerCode}&accessKey={_config.AccessKey}&requestId={requestId}&amount={amount}&orderId={orderId}&orderInfo={model.Description}&returnUrl={_config.ReturnUrl}&notifyUrl={_config.NotifyUrl}&extraData=";
            var signature = ComputeHmacSHA256(rawHash, _config.SecretKey);

            // Tạo request object gửi đến Momo
            var paymentRequest = new
            {
                partnerCode = _config.PartnerCode,
                accessKey = _config.AccessKey,
                requestId,
                amount,
                orderId,
                orderInfo = model.Description,
                returnUrl = _config.ReturnUrl,
                notifyUrl = _config.NotifyUrl,
                requestType = _config.RequestType,
                extraData = "",
                signature
            };

            // Gửi POST đến Momo
            using var httpClient = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(_config.MomoApiUrl, content);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Momo API Error: {responseJson}");
            }
            Console.WriteLine("👉 Momo API Response:");
            Console.WriteLine(responseJson);
            var result = JsonSerializer.Deserialize<MomoCreatePaymentResponseModel>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // 🔥 Cho phép map tên không phân biệt chữ hoa thường
                });
            // Ghi lại transaction
            var transaction = new TransactionLogs
            {
                MomoOrderId = orderId,
                Amount = model.Amount,
                Description = model.Description,
                Status = "Pending",
                PaymentMethod = "Momo",
                UserId = model.UserId,
                TransactionTypeId = model.TransactionTypeId,
                CreatedBy = model.UserId.ToString(),
                CreatedTime = DateTimeOffset.UtcNow,
                CampaignId = model.CampaignId
            };
            await _unitOfWork.GetRepository<TransactionLogs>().CreateAsync(transaction);
            await _unitOfWork.SaveAsync();
            return result!;
        }

        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
        {
            string GetValue(string key) =>
                collection.ContainsKey(key) ? collection[key].ToString() : "";

            int TryParseInt(string key) =>
                int.TryParse(GetValue(key), out var result) ? result : 0;

            long TryParseLong(string key) =>
                long.TryParse(GetValue(key), out var result) ? result : 0;

            return new MomoExecuteResponseModel
            {
                PartnerCode = GetValue("partnerCode"),
                OrderId = GetValue("orderId"),
                RequestId = GetValue("requestId"),
                Amount = GetValue("amount"),
                OrderInfo = GetValue("orderInfo"),
                OrderType = GetValue("orderType"),
                TransId = GetValue("transId"),
                ResultCode = TryParseInt("resultCode"),
                Message = GetValue("message"),
                PayType = GetValue("payType"),
                ResponseTime = TryParseLong("responseTime"),
                ExtraData = GetValue("extraData"),
                Signature = GetValue("signature")
            };
        }


        // 🔐 Hàm ký SHA256
        private static string ComputeHmacSHA256(string rawData, string secretKey)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public async Task HandleSuccessfulPaymentAsync(MomoExecuteResponseModel response)
        {
            var transactionRepo = _unitOfWork.GetRepository<TransactionLogs>();
            var campaignRepo = _unitOfWork.GetRepository<Campaigns>();

            // Tìm transaction theo OrderId
            var transaction = await transactionRepo.GetOneAsync(x => x.MomoOrderId == response.OrderId);
            if (transaction == null) return;

            // Nếu đã xử lý trước đó thì không làm lại
            if (transaction.Status == "Success") return;

            // Cập nhật trạng thái
            transaction.Status = "Success";

            // Nếu có CampaignId -> cộng tiền
            if (!string.IsNullOrEmpty(transaction.CampaignId))
            {
                var campaignId = transaction.CampaignId;
                var campaign = await campaignRepo.GetOneAsync(x => x.Id == campaignId && !x.IsDeleted);

                if (campaign != null)
                {
                    campaign.RaisedAmount += (decimal)transaction.Amount;
                    await campaignRepo.UpdateAsync(campaign);
                }
            }

            // Cập nhật transaction
            await transactionRepo.UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public async Task HandleFailedPaymentAsync(MomoExecuteResponseModel response)
        {
            var transactionRepo = _unitOfWork.GetRepository<TransactionLogs>();

            var transaction = await transactionRepo.GetOneAsync(x => x.MomoOrderId == response.OrderId);
            if (transaction == null) return;

            // Nếu đã xử lý rồi thì bỏ qua
            if (transaction.Status == "Failed" || transaction.Status == "Success") return;

            // Cập nhật trạng thái thất bại
            transaction.Status = "Failed";
            await transactionRepo.UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }


    }
}
