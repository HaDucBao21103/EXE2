using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IPostService
    {
        #region Non-Async Methods

        IEnumerable<PostsResponse> GetAll();
        PostsResponse? GetById(string id);
        BasePaginatedList<PostsResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<PostsResponse> Search(string? keyword);
        bool Create(PostsCreateRequest post);
        bool Update(PostsRequest post);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<PostsResponse>> GetAllAsync();
        Task<PostsResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<PostsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<PostsResponse>> SearchAsync(string? keyword);
        Task<bool> CreateAsync(PostsCreateRequest post);
        Task<bool> UpdateAsync(PostsRequest post);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateStatusAsync(string status, string id);
        #endregion
    }
}
