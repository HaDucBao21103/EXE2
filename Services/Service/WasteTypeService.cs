using AutoMapper;
using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class WasteTypeService : IWasteTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WasteTypeService(IUnitOfWork unitOfWork, IMapper mapper)
            => (_unitOfWork, _mapper) = (unitOfWork, mapper);

        #region Async Methods

        public async Task<IEnumerable<WasteTypesResponse>> GetAllAsync()
        {
            var data = await _unitOfWork.GetRepository<WasteTypes>().GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<WasteTypesResponse>>(data);
        }

        public async Task<WasteTypesResponse?> GetByIdAsync(string id)
        {
            var item = await _unitOfWork.GetRepository<WasteTypes>().GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<WasteTypesResponse>(item);
        }

        public async Task<BasePaginatedList<WasteTypesResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var items = await _unitOfWork.GetRepository<WasteTypes>().GetAllAsync(x => !x.IsDeleted);
            var mapped = _mapper.Map<List<WasteTypesResponse>>(items);
            return new BasePaginatedList<WasteTypesResponse>(mapped, mapped.Count, pageNumber, pageSize);
        }

        public async Task<IEnumerable<WasteTypesResponse>> SearchAsync(string keyword)
        {
            var result = _unitOfWork.GetRepository<WasteTypes>().Entities
                .Where(x => !x.IsDeleted &&
                    (x.TypeName.ToLower().Contains(keyword.ToLower()) || string.IsNullOrEmpty(keyword) ||
                     x.Description.ToLower().Contains(keyword.ToLower()) || string.IsNullOrEmpty(keyword)))
                .ToList();
            return await Task.FromResult(_mapper.Map<IEnumerable<WasteTypesResponse>>(result));
        }

        public async Task<bool> CreateAsync(WasteTypesRequest request)
        {
            var entity = _mapper.Map<WasteTypes>(request);
            await _unitOfWork.GetRepository<WasteTypes>().CreateAsync(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(WasteTypesRequest request)
        {
            var entity = _mapper.Map<WasteTypes>(request);
            await _unitOfWork.GetRepository<WasteTypes>().UpdateAsync(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _unitOfWork.GetRepository<WasteTypes>().GetByIdAsync(id);
            if (item == null) return false;

            item.IsDeleted = true;
            await _unitOfWork.GetRepository<WasteTypes>().UpdateAsync(item);
            return await _unitOfWork.SaveAsync() > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<WasteTypesResponse> GetAll()
        {
            var data = _unitOfWork.GetRepository<WasteTypes>().GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<WasteTypesResponse>>(data);
        }

        public WasteTypesResponse? GetById(string id)
        {
            var item = _unitOfWork.GetRepository<WasteTypes>().GetOne(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<WasteTypesResponse>(item);
        }

        public BasePaginatedList<WasteTypesResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var items = _unitOfWork.GetRepository<WasteTypes>().GetAll(x => !x.IsDeleted).ToList();
            var mapped = _mapper.Map<List<WasteTypesResponse>>(items);
            return new BasePaginatedList<WasteTypesResponse>(mapped, mapped.Count, pageNumber, pageSize);
        }

        public IEnumerable<WasteTypesResponse> Search(string keyword)
        {
            var result = _unitOfWork.GetRepository<WasteTypes>().Entities
                .Where(x => !x.IsDeleted &&
                    (x.TypeName.ToLower().Contains(keyword.ToLower()) || string.IsNullOrEmpty(keyword) ||
                     x.Description.ToLower().Contains(keyword.ToLower()) || string.IsNullOrEmpty(keyword)))
                .ToList();
            return _mapper.Map<IEnumerable<WasteTypesResponse>>(result);
        }

        public bool Create(WasteTypesRequest request)
        {
            var entity = _mapper.Map<WasteTypes>(request);
            _unitOfWork.GetRepository<WasteTypes>().Create(entity);
            return _unitOfWork.Save() > 0;
        }

        public bool Update(WasteTypesRequest request)
        {
            var entity = _mapper.Map<WasteTypes>(request);
            _unitOfWork.GetRepository<WasteTypes>().Update(entity);
            return _unitOfWork.Save() > 0;
        }

        public bool Delete(string id)
        {
            var item = _unitOfWork.GetRepository<WasteTypes>().GetById(id);
            if (item == null) return false;

            item.IsDeleted = true;
            _unitOfWork.GetRepository<WasteTypes>().Update(item);
            return _unitOfWork.Save() > 0;
        }

        #endregion
    }
}
