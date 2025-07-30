using Core.Base;
using System.Linq.Expressions;

namespace Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        #region Non-Async Method
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        T? GetById(object id);
        T? GetByGuidId(Guid id);
        T? GetOne(Expression<Func<T, bool>> predicate);
        BasePaginatedList<T> GetPagging(IQueryable<T> query, int index, int pageSize);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        #endregion

        #region Async Method
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(object id);
        Task<T?> GetByGuidIdAsync(Guid id);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<BasePaginatedList<T>> GetPaggingAsync(IQueryable<T> query, int index, int pageSize);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        #endregion
    }
}
