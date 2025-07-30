using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IWasteTypeService
    {
        #region Non-Async Methods
        IEnumerable<WasteTypesResponse> GetAll();
        WasteTypesResponse? GetById(string id);
        BasePaginatedList<WasteTypesResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<WasteTypesResponse> Search(string keyword);
        bool Create(WasteTypesRequest request);
        bool Update(WasteTypesRequest request);
        bool Delete(string id);
        #endregion

        #region Async Methods
        Task<IEnumerable<WasteTypesResponse>> GetAllAsync();
        Task<WasteTypesResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<WasteTypesResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<WasteTypesResponse>> SearchAsync(string keyword);
        Task<bool> CreateAsync(WasteTypesRequest request);
        Task<bool> UpdateAsync(WasteTypesRequest request);
        Task<bool> DeleteAsync(string id);
        #endregion
    }
}
