using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IBlogTypeService
    {
        #region Non-Async Methods

        IEnumerable<BlogTypesResponse> GetAll();
        BlogTypesResponse? GetById(string id);
        BasePaginatedList<BlogTypesResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<BlogTypesResponse> Search(string keyword);
        bool Create(BlogTypesRequest blogType);
        bool Update(BlogTypesRequest blogType);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<BlogTypesResponse>> GetAllAsync();
        Task<BlogTypesResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<BlogTypesResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<BlogTypesResponse>> SearchAsync(string keyword);
        Task<bool> CreateAsync(BlogTypesRequest blogType);
        Task<bool> UpdateAsync(BlogTypesRequest blogType);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
