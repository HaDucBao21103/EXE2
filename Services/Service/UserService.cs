using AutoMapper;
using BusinessObject;
using Microsoft.AspNetCore.Identity;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Security.Claims;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersResponse>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.GetRepository<Users>().GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<UsersResponse>>(users);
        }

        public async Task<UsersResponse?> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var usersId = user.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            if (usersId == null) return null;

            var currentUser = await _unitOfWork.GetRepository<Users>().GetOneAsync(u => !u.IsDeleted && u.Id == Guid.Parse(usersId));
            return currentUser == null ? null : _mapper.Map<UsersResponse>(currentUser);
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var exitUser = await _unitOfWork.GetRepository<Users>().GetOneAsync(u => u.UserName == request.UserName || u.Email == request.Email);
                if (exitUser != null)
                {
                    return false;
                }

                var role = await _unitOfWork.GetRepository<Roles>().GetOneAsync(r => r.Name == "User");
                if (role == null)
                {
                    var newRole = new Roles
                    {
                        Name = "User",
                        NormalizedName = "USER",
                        CreatedBy = "System"
                    };
                    _unitOfWork.GetRepository<Roles>().Create(newRole);
                    role = newRole;
                }

                var passwordHasher = new PasswordHasher<Users>();
                var user = _mapper.Map<Users>(request);
                user.PasswordHash = passwordHasher.HashPassword(null, request.Password);
                user.RoleId = role.Id;
                user.CreatedBy = request.UserName;
                user.LastUpdatedBy = request.UserName;

                _unitOfWork.GetRepository<Users>().Create(user);
                var result = await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return result > 0;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                // Ghi log ra console hoặc logger tùy hệ thống bạn dùng
                Console.WriteLine($"[RegisterAsync Error] {ex.Message}");

                // (Tùy chọn) ném lại exception để controller hiển thị rõ lỗi
                throw new Exception($"Đăng ký thất bại: {ex.Message}");
            }
        }

        public async Task<bool> UpdateProfileAsync(UsersRequest request)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var currentUser = await _unitOfWork.GetRepository<Users>().GetOneAsync(p => p.Id == request.Id);
                if (currentUser == null) return false;

                _mapper.Map(request, currentUser);
                currentUser.LastUpdatedBy = request.FullName;

                await _unitOfWork.GetRepository<Users>().UpdateAsync(currentUser);
                var result = await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return result > 0;
            }
            catch
            {
                _unitOfWork.RollBack();
                return false;
            }
        }

        public async Task<bool> UpdateRoleAsync(UsersRequestRole request)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var currentUser = await _unitOfWork.GetRepository<Users>().GetOneAsync(p => p.Id == request.UserId);
                if (currentUser == null) return false;

                currentUser.RoleId = (Guid)request.RoleId;
                await _unitOfWork.GetRepository<Users>().UpdateAsync(currentUser);
                var result = await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return result > 0;
            }
            catch
            {
                _unitOfWork.RollBack();
                return false;
            }
        }
    }
}
