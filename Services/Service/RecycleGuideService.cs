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
    public class RecycleGuideService : IRecycleGuideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<RecycleGuides> _recycleRepo;

        public RecycleGuideService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _recycleRepo = _unitOfWork.GetRepository<RecycleGuides>();
        }

        #region Async Methods

        public async Task<IEnumerable<RecycleGuidesResponse>> GetAllAsync()
        {
            var result = await _recycleRepo.GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<RecycleGuidesResponse>>(result);
        }

        public async Task<RecycleGuidesResponse?> GetByIdAsync(string id)
        {
            var result = await _recycleRepo.GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<RecycleGuidesResponse>(result);
        }

        public async Task<BasePaginatedList<RecycleGuidesResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var query = _recycleRepo.Entities.Where(x => !x.IsDeleted);
            var result = await _recycleRepo.GetPaggingAsync(query, pageNumber, pageSize);
            var mapped = _mapper.Map<List<RecycleGuidesResponse>>(result.Items);
            return new BasePaginatedList<RecycleGuidesResponse>(mapped, result.TotalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<RecycleGuidesResponse>> SearchAsync(string? keyword)
        {
            var query = _recycleRepo.Entities
                .Include(x => x.Waste)
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Waste.Name.ToLower().Contains(keyword.ToLower())
                ));

            return _mapper.Map<IEnumerable<RecycleGuidesResponse>>(query.AsEnumerable());
        }

        public async Task<bool> CreateAsync(RecycleGuidesRequest request)
        {
            var entity = _mapper.Map<RecycleGuides>(request);
            await _recycleRepo.CreateAsync(entity);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(RecycleGuidesRequest request)
        {
            var existing = await _recycleRepo.GetByIdAsync(request.Id);
            if (existing == null) return false;

            _mapper.Map(request, existing);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _recycleRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.IsDeleted = true;
            await _recycleRepo.UpdateAsync(entity);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<RecycleGuidesResponse> GetAll()
        {
            var result = _recycleRepo.GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<RecycleGuidesResponse>>(result);
        }

        public RecycleGuidesResponse? GetById(string id)
        {
            var result = _recycleRepo.GetOne(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<RecycleGuidesResponse>(result);
        }

        public IEnumerable<RecycleGuidesResponse> Search(string? keyword)
        {
            var result = _recycleRepo.Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Waste.Name.ToLower().Contains(keyword.ToLower())
                )).AsEnumerable();

            return _mapper.Map<IEnumerable<RecycleGuidesResponse>>(result);
        }

        public bool Create(RecycleGuidesRequest request)
        {
            var entity = _mapper.Map<RecycleGuides>(request);
            _recycleRepo.Create(entity);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(RecycleGuidesRequest request)
        {
            var existing = _recycleRepo.GetById(request.Id);
            if (existing == null) return false;

            _mapper.Map(request, existing);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var entity = _recycleRepo.GetById(id);
            if (entity == null) return false;

            entity.IsDeleted = true;
            _recycleRepo.Update(entity);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public BasePaginatedList<RecycleGuidesResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var query = _recycleRepo.Entities.Where(x => !x.IsDeleted);
            var result = _recycleRepo.GetPagging(query, pageNumber, pageSize);
            var mapped = _mapper.Map<List<RecycleGuidesResponse>>(result.Items);
            return new BasePaginatedList<RecycleGuidesResponse>(mapped, result.TotalItems, pageNumber, pageSize);
        }

        #endregion
    }
}
