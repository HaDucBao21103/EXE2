using AutoMapper;
using BusinessObject;
using Core.Base;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class RecycleLocationService : IRecycleLocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecycleLocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Async Methods

        public async Task<IEnumerable<RecycleLocationsResponse>> GetAllAsync()
        {
            var list = await _unitOfWork.GetRepository<RecycleLocations>().GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<RecycleLocationsResponse>>(list);
        }

        public async Task<RecycleLocationsResponse?> GetByIdAsync(string id)
        {
            var location = await _unitOfWork.GetRepository<RecycleLocations>().GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return location != null ? _mapper.Map<RecycleLocationsResponse>(location) : null;
        }

        public async Task<BasePaginatedList<RecycleLocationsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var items = await _unitOfWork.GetRepository<RecycleLocations>().GetAllAsync(x => !x.IsDeleted);
            var mapped = _mapper.Map<List<RecycleLocationsResponse>>(items);
            return new BasePaginatedList<RecycleLocationsResponse>(mapped, mapped.Count, pageNumber, pageSize);
        }
        public async Task<IEnumerable<RecycleLocationsResponse>> GetByWastesTypeId(string id)
        {
            var result = _unitOfWork.GetRepository<RecycleLocations>().Entities
                .Include(x => x.WasteType)
                .Where(x => !x.IsDeleted && x.WasteTypeId == id);
            return _mapper.Map<IEnumerable<RecycleLocationsResponse>>(result);
        }
        public async Task<IEnumerable<RecycleLocationsResponse>> SearchAsync(string keyword)
        {
            var result = _unitOfWork.GetRepository<RecycleLocations>().Entities
                .Include(x => x.WasteType)
                .Where(x => !x.IsDeleted &&
                    (
                        x.Name.ToLower().Contains(keyword.ToLower()) ||
                        x.Address.ToLower().Contains(keyword.ToLower()) ||
                        x.Description.ToLower().Contains(keyword.ToLower()) ||
                        string.IsNullOrEmpty(keyword)
                    ));
            return _mapper.Map<IEnumerable<RecycleLocationsResponse>>(result);
        }

        public async Task<bool> CreateAsync(RecycleLocationsRequest request)
        {
            var location = _mapper.Map<RecycleLocations>(request);
            await _unitOfWork.GetRepository<RecycleLocations>().CreateAsync(location);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RecycleLocationsRequest request)
        {
            var location = _mapper.Map<RecycleLocations>(request);
            await _unitOfWork.GetRepository<RecycleLocations>().UpdateAsync(location);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _unitOfWork.GetRepository<RecycleLocations>().GetByIdAsync(id);
            if (item == null) return false;

            item.IsDeleted = true;
            await _unitOfWork.GetRepository<RecycleLocations>().UpdateAsync(item);
            return await _unitOfWork.SaveAsync() > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<RecycleLocationsResponse> GetAll()
        {
            var list = _unitOfWork.GetRepository<RecycleLocations>().GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<RecycleLocationsResponse>>(list);
        }

        public RecycleLocationsResponse? GetById(string id)
        {
            var location = _unitOfWork.GetRepository<RecycleLocations>().GetOne(x => x.Id == id && !x.IsDeleted);
            return location != null ? _mapper.Map<RecycleLocationsResponse>(location) : null;
        }

        public BasePaginatedList<RecycleLocationsResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var items = _unitOfWork.GetRepository<RecycleLocations>().GetAll(x => !x.IsDeleted).ToList();
            var mapped = _mapper.Map<List<RecycleLocationsResponse>>(items);
            return new BasePaginatedList<RecycleLocationsResponse>(mapped, mapped.Count, pageNumber, pageSize);
        }

        public IEnumerable<RecycleLocationsResponse> Search(string keyword)
        {
            var result = _unitOfWork.GetRepository<RecycleLocations>().Entities
                .Where(x => !x.IsDeleted &&
                    (
                        x.Name.ToLower().Contains(keyword.ToLower()) ||
                        x.Address.ToLower().Contains(keyword.ToLower()) ||
                        x.Description.ToLower().Contains(keyword.ToLower()) ||
                        string.IsNullOrEmpty(keyword)
                    ));
            return _mapper.Map<IEnumerable<RecycleLocationsResponse>>(result);
        }

        public bool Create(RecycleLocationsRequest request)
        {
            var location = _mapper.Map<RecycleLocations>(request);
            _unitOfWork.GetRepository<RecycleLocations>().Create(location);
            return _unitOfWork.Save() > 0;
        }

        public bool Update(RecycleLocationsRequest request)
        {
            var location = _mapper.Map<RecycleLocations>(request);
            _unitOfWork.GetRepository<RecycleLocations>().Update(location);
            return _unitOfWork.Save() > 0;
        }

        public bool Delete(string id)
        {
            var item = _unitOfWork.GetRepository<RecycleLocations>().GetById(id);
            if (item == null) return false;

            item.IsDeleted = true;
            _unitOfWork.GetRepository<RecycleLocations>().Update(item);
            return _unitOfWork.Save() > 0;
        }

        #endregion
    }
}
