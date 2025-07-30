using Core.Base;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        #region Non-Async Methods

        IEnumerable<CommentsResponse> GetAll();
        CommentsResponse? GetById(string id);
        BasePaginatedList<CommentsResponse> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<CommentsResponse> Search(string? keyword);
        bool Create(CommentsRequest comment);
        bool Update(CommentsRequest comment);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<CommentsResponse>> GetAllAsync();
        Task<CommentsResponse?> GetByIdAsync(string id);
        Task<BasePaginatedList<CommentsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CommentsResponse>> SearchAsync(string? keyword);
        Task<bool> CreateAsync(CommentsRequest comment);
        Task<bool> UpdateAsync(CommentsRequest comment);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
