using BusinessObject;
using Core.Base;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface ICampaignsService
    {
        #region Non-Async Methods
        IEnumerable<Campaigns> GetAll();
        Campaigns? GetById(string id);
        BasePaginatedList<Campaigns> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<Campaigns> Search(string keyword);
        bool Create(Campaigns campaign);
        bool Update(Campaigns campaign);
        bool Delete(string id);
        #endregion

        #region Async Methods
        Task<List<CampaignTransactionSummaryResponse>> GetCampaignTransactionSummaryAsync();
        Task<CampaignTransactionSummaryResponse?> GetCampaignTransactionSummaryByIdAsync(string id);
        Task<IEnumerable<Campaigns>> GetAllAsync();
        Task<Campaigns?> GetByIdAsync(string id);
        Task<BasePaginatedList<Campaigns>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Campaigns>> SearchAsync(string keyword);
        Task<bool> CreateAsync(Campaigns campaign);
        Task<bool> UpdateAsync(Campaigns campaign);
        Task<bool> DeleteAsync(string id);
        #endregion
    }
}
