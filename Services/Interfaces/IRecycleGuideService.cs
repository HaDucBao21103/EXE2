using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IRecycleGuideService
    {
        #region Non-Async Methods

        IEnumerable<RecycleGuidesResponse> GetAll();
        RecycleGuidesResponse? GetById(string id);
        BasePaginatedList<RecycleGuidesResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<RecycleGuidesResponse> Search(string? keyword);
        bool Create(RecycleGuidesRequest guide);
        bool Update(RecycleGuidesRequest guide);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<RecycleGuidesResponse>> GetAllAsync();
        Task<RecycleGuidesResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<RecycleGuidesResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<RecycleGuidesResponse>> SearchAsync(string? keyword);
        Task<bool> CreateAsync(RecycleGuidesRequest guide);
        Task<bool> UpdateAsync(RecycleGuidesRequest guide);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
