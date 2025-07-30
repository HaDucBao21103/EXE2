using AutoMapper;
using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Roles> _rolesRepo; // Repository for Roles entity

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rolesRepo = _unitOfWork.GetRepository<Roles>(); // Get the repository for Roles
        }

        #region Async Methods

        public async Task<IEnumerable<RolesResponse>> GetAllAsync()
        {
            var result = await _rolesRepo.GetAllAsync(x => !x.IsDeleted);
            var item = _mapper.Map<IEnumerable<RolesResponse>>(result);
            return item;
        }

        public async Task<RolesResponse?> GetByIdAsync(string id)
        {
            var result = await _rolesRepo.GetOneAsync(x => x.Id.ToString() == id && !x.IsDeleted);
            var response = _mapper.Map<RolesResponse>(result);
            return response;
        }


        public async Task<bool> CreateAsync(RolesRequest role)
        {
            var request = _mapper.Map<Roles>(role);
            var newRole = new Roles
            {
                Name = role.Name,
                NormalizedName = role.Name.ToUpper(),
                CreatedBy = "System"
            };
            await _rolesRepo.CreateAsync(newRole);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(string id, RolesRequest role)
        {
            var exitItem = await _rolesRepo.GetOneAsync(x => x.Id.ToString() == id && !x.IsDeleted);
            if (exitItem == null)
            {
                return false;
            }
            _mapper.Map(role, exitItem);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid roleGuid))
            {
                return false;
            }
            var item = await _rolesRepo.GetByIdAsync(roleGuid);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true; // Assuming 'IsDeleted' property exists on your Roles entity
            await _rolesRepo.UpdateAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<RolesResponse> GetAll()
        {
            var result = _rolesRepo.GetAll(x => !x.IsDeleted);
            var response = _mapper.Map<IEnumerable<RolesResponse>>(result);
            return response;
        }

        public RolesResponse? GetById(string id)
        {
            var result = _rolesRepo.GetOne(x => x.Id.ToString() == id && !x.IsDeleted);
            var response = _mapper.Map<RolesResponse>(result);
            return response;
        }

        public BasePaginatedList<RolesResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var result = _rolesRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = _rolesRepo.GetPagging(result, pageNumber, pageSize);
            var response = _mapper.Map<List<RolesResponse>>(pagingResult.Items);
            return new BasePaginatedList<RolesResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public IEnumerable<RolesResponse> Search(string? keyword)
        {
            var result = _rolesRepo
                .Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Name.ToLower().Contains(keyword.ToLower()) // Assuming 'Name' for Role
                                                                    // Add other searchable properties of your 'Roles' entity here if needed
                ))
                .AsEnumerable();

            var response = _mapper.Map<IEnumerable<RolesResponse>>(result);
            return response;
        }

        public bool Create(RolesRequest role)
        {
            var request = _mapper.Map<Roles>(role);
            _rolesRepo.Create(request);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(string id, RolesRequest role)
        {
            var exitItem = _rolesRepo.GetOne(x => x.Id.ToString() == id && !x.IsDeleted);
            if (exitItem == null)
            {
                return false;
            }
            _mapper.Map(role, exitItem);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var item = _rolesRepo.GetById(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true; // Assuming 'IsDeleted' property exists on your Roles entity
            _rolesRepo.Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        #endregion
    }
}

