using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IRecycleLocationService
    {
        #region Non-Async Methods

        IEnumerable<RecycleLocationsResponse> GetAll();
        RecycleLocationsResponse? GetById(string id);
        BasePaginatedList<RecycleLocationsResponse> GetPaginatedList(int pageNumber, int pageSize);
        Task<IEnumerable<RecycleLocationsResponse>> GetByWastesTypeId(string id);
        IEnumerable<RecycleLocationsResponse> Search(string keyword);
        bool Create(RecycleLocationsRequest location);
        bool Update(RecycleLocationsRequest location);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<RecycleLocationsResponse>> GetAllAsync();
        Task<RecycleLocationsResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<RecycleLocationsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<RecycleLocationsResponse>> SearchAsync(string keyword);
        Task<bool> CreateAsync(RecycleLocationsRequest location);
        Task<bool> UpdateAsync(RecycleLocationsRequest location);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
