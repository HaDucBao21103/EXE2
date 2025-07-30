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
    public class WasteService : IWasteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WasteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Async Methods

        public async Task<IEnumerable<WasteResponse>> GetAllAsync()
        {
            var wastes = await _unitOfWork.GetRepository<Wastes>().GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<WasteResponse>>(wastes);
        }

        public async Task<WasteResponse?> GetByIdAsync(string id)
        {
            var waste = await _unitOfWork.GetRepository<Wastes>().GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<WasteResponse>(waste);
        }
        public async Task<IEnumerable<WasteResponse>> GetWastesByWastesTypeId(string id)
        {
            var waste = _unitOfWork.GetRepository<Wastes>().Entities
                .Include(x => x.WasteType)
                .Where(x => !x.IsDeleted && x.WasteTypeId == id);
            return _mapper.Map<IEnumerable<WasteResponse>>(waste);
        }
        public async Task<IEnumerable<WasteResponse>> SearchAsync(string keyword)
        {
            var result = _unitOfWork.GetRepository<Wastes>().Entities
                .Include(x => x.WasteType)
                .Where(x => !x.IsDeleted &&
                    (x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower()) || x.WasteType.TypeName.ToLower().Contains(keyword.ToLower()))
                );

            return _mapper.Map<IEnumerable<WasteResponse>>(result);
        }

        public async Task<bool> CreateAsync(WasteRequest request)
        {
            var waste = _mapper.Map<Wastes>(request);
            await _unitOfWork.GetRepository<Wastes>().CreateAsync(waste);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(WasteRequest request)
        {
            var existing = await _unitOfWork.GetRepository<Wastes>().GetByIdAsync(request.Id);
            if (existing == null) return false;

            _mapper.Map(request, existing);
            await _unitOfWork.GetRepository<Wastes>().UpdateAsync(existing);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var waste = await _unitOfWork.GetRepository<Wastes>().GetByIdAsync(id);
            if (waste == null) return false;

            waste.IsDeleted = true;
            await _unitOfWork.GetRepository<Wastes>().UpdateAsync(waste);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<BasePaginatedList<WasteResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var wastes = await _unitOfWork.GetRepository<Wastes>().GetAllAsync(x => !x.IsDeleted);
            var response = _mapper.Map<List<WasteResponse>>(wastes.ToList());
            return new BasePaginatedList<WasteResponse>(response, response.Count, pageNumber, pageSize);
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<WasteResponse> GetAll()
        {
            var wastes = _unitOfWork.GetRepository<Wastes>().GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<WasteResponse>>(wastes);
        }

        public WasteResponse? GetById(string id)
        {
            var waste = _unitOfWork.GetRepository<Wastes>().GetOne(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<WasteResponse>(waste);
        }

        public IEnumerable<WasteResponse> Search(string keyword)
        {
            var result = _unitOfWork.GetRepository<Wastes>().Entities
                .Where(x => !x.IsDeleted &&
                    (x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower()) || x.WasteType.TypeName.ToLower().Contains(keyword.ToLower()))
                );

            return _mapper.Map<IEnumerable<WasteResponse>>(result);
        }

        public bool Create(WasteRequest request)
        {
            var waste = _mapper.Map<Wastes>(request);
            _unitOfWork.GetRepository<Wastes>().Create(waste);
            return _unitOfWork.Save() > 0;
        }

        public bool Update(WasteRequest request)
        {
            var waste = _unitOfWork.GetRepository<Wastes>().GetById(request.Id);
            if (waste == null) return false;

            _mapper.Map(request, waste);
            _unitOfWork.GetRepository<Wastes>().Update(waste);
            return _unitOfWork.Save() > 0;
        }

        public bool Delete(string id)
        {
            var waste = _unitOfWork.GetRepository<Wastes>().GetById(id);
            if (waste == null) return false;

            waste.IsDeleted = true;
            _unitOfWork.GetRepository<Wastes>().Update(waste);
            return _unitOfWork.Save() > 0;
        }

        #endregion
    }
}
