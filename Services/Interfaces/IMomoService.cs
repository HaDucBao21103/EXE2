using Microsoft.AspNetCore.Http;
using ViewModels;
using ViewModels.Momo;

namespace Services.Interfaces
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(TransactionInfoModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
        Task HandleSuccessfulPaymentAsync(MomoExecuteResponseModel response);
        Task HandleFailedPaymentAsync(MomoExecuteResponseModel response);
    }
}