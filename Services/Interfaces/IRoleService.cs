using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IRoleService
    {
        #region Non-Async Methods

        IEnumerable<RolesResponse> GetAll();
        RolesResponse? GetById(string id);
        bool Create(RolesRequest role);
        bool Update(string id, RolesRequest role);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<RolesResponse>> GetAllAsync();
        Task<RolesResponse?> GetByIdAsync(string id);
        Task<bool> CreateAsync(RolesRequest role);
        Task<bool> UpdateAsync(string id, RolesRequest role);
        Task<bool> DeleteAsync(string id);
        #endregion
    }
}
