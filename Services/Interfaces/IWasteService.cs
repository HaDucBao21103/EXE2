using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IWasteService
    {
        #region Non-Async Methods

        IEnumerable<WasteResponse> GetAll();
        WasteResponse? GetById(string id);
        IEnumerable<WasteResponse> Search(string keyword);
        bool Create(WasteRequest request);
        bool Update(WasteRequest request);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<WasteResponse>> GetAllAsync();
        Task<WasteResponse?> GetByIdAsync(string id);
        Task<IEnumerable<WasteResponse>> GetWastesByWastesTypeId(string id);
        Task<IEnumerable<WasteResponse>> SearchAsync(string keyword);
        Task<bool> CreateAsync(WasteRequest request);
        Task<bool> UpdateAsync(WasteRequest request);
        Task<bool> DeleteAsync(string id);
        Task<BasePaginatedList<WasteResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);

        #endregion
    }
}
