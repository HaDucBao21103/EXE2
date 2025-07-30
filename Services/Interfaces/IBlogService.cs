using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IBlogService
    {
        #region Non-Async Methods

        IEnumerable<BlogsResponse> GetAll();
        BlogsResponse? GetById(string id);
        BasePaginatedList<BlogsResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<BlogsResponse> Search(string? keyword);
        bool Create(BlogsRequest blog);
        bool Update(BlogsRequest blog);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<BlogsResponse>> GetAllAsync();
        Task<BlogsResponse?> GetByIdAsync(string id);
        Task<IEnumerable<BlogsResponse>> GetByBlogTypeIdAsync(string idBlogType);
        Task<BasePaginatedList<BlogsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<BlogsResponse>> SearchAsync(string? keyword);
        Task<bool> CreateAsync(BlogsRequest blog);
        Task<bool> UpdateAsync(BlogsRequest blog);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
