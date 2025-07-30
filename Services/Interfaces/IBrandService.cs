using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IBrandService
    {
        #region Non-Async Methods

        IEnumerable<BrandsResponse> GetAll();
        BrandsResponse? GetById(string id);
        BasePaginatedList<BrandsResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<BrandsResponse> Search(string keyword);
        bool Create(BrandsRequest brand);
        bool Update(BrandsRequest brand);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<BrandsResponse>> GetAllAsync();
        Task<BrandsResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<BrandsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<BrandsResponse>> SearchAsync(string keyword);
        Task<bool> CreateAsync(BrandsRequest brand);
        Task<bool> UpdateAsync(BrandsRequest brand);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
