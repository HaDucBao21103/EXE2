using System.Security.Claims;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UsersResponse>> GetAllUsersAsync();
        Task<UsersResponse?> GetCurrentUserAsync(ClaimsPrincipal user);

        Task<bool> RegisterAsync(RegisterRequest request);
        Task<bool> UpdateProfileAsync(UsersRequest request);
        Task<bool> UpdateRoleAsync(UsersRequestRole request);
    }
}
