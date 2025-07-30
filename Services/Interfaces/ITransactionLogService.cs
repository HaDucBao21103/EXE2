using BusinessObject;
using Core.Base;
using System.Security.Claims;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface ITransactionLogService
    {
        Task<IEnumerable<TransactionResponse>?> GetAllTransactionLogsAsync();
        Task<IEnumerable<TransactionResponse>?> GetAllTransactionLogsByCampaign(string campaginId);
        Task<IEnumerable<TransactionResponse>?> GetTransactionLogsOfUser(Guid userId);
        Task<BasePaginatedList<TransactionResponse>?> GetTransactionLogsOfUsersPaging(ClaimsPrincipal claim, int currentPage, int pageSize);
        Task<BasePaginatedList<TransactionResponse>?> GetAllTransactionWithPaging(int currentPage, int pageSize);
        Task<object> GetAllTransactionByYear(int year);
        Task<object> GetAllTransactionByYearAndCampaign(int year, string campaignId);
    }
}
