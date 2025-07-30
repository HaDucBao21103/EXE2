using AutoMapper;
using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Brands> _brandRepo;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandRepo = _unitOfWork.GetRepository<Brands>();
        }

        #region Async Methods

        public async Task<IEnumerable<BrandsResponse>> GetAllAsync()
        {
            var result = await _brandRepo.GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<BrandsResponse>>(result);
        }

        public async Task<BrandsResponse?> GetByIdAsync(string id)
        {
            var result = await _brandRepo.GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<BrandsResponse>(result);
        }

        public async Task<BasePaginatedList<BrandsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var query = _brandRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = await _brandRepo.GetPaggingAsync(query, pageNumber, pageSize);
            var response = _mapper.Map<List<BrandsResponse>>(pagingResult.Items);
            return new BasePaginatedList<BrandsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<BrandsResponse>> SearchAsync(string keyword)
        {
            var result = _brandRepo.Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Name.Contains(keyword)
                    || x.Description.Contains(keyword)))
                .AsEnumerable();

            var response = _mapper.Map<IEnumerable<BrandsResponse>>(result);
            return await Task.FromResult(response);
        }

        public async Task<bool> CreateAsync(BrandsRequest brand)
        {
            var entity = _mapper.Map<Brands>(brand);
            await _brandRepo.CreateAsync(entity);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(BrandsRequest brand)
        {
            var existing = await _brandRepo.GetOneAsync(x => x.Id == brand.Id && !x.IsDeleted);
            if (existing == null) return false;

            _mapper.Map(brand, existing);
            await _brandRepo.UpdateAsync(existing);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _brandRepo.GetByIdAsync(id);
            if (item == null) return false;

            item.IsDeleted = true;
            await _brandRepo.UpdateAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<BrandsResponse> GetAll()
        {
            var result = _brandRepo.GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<BrandsResponse>>(result);
        }

        public BrandsResponse? GetById(string id)
        {
            var result = _brandRepo.GetOne(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<BrandsResponse>(result);
        }

        public BasePaginatedList<BrandsResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var query = _brandRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = _brandRepo.GetPagging(query, pageNumber, pageSize);
            var response = _mapper.Map<List<BrandsResponse>>(pagingResult.Items);
            return new BasePaginatedList<BrandsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public IEnumerable<BrandsResponse> Search(string keyword)
        {
            var result = _brandRepo.Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Name.Contains(keyword)
                    || x.Description.Contains(keyword)))
                .AsEnumerable();

            return _mapper.Map<IEnumerable<BrandsResponse>>(result);
        }

        public bool Create(BrandsRequest brand)
        {
            var entity = _mapper.Map<Brands>(brand);
            _brandRepo.Create(entity);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(BrandsRequest brand)
        {
            var existing = _brandRepo.GetOne(x => x.Id == brand.Id && !x.IsDeleted);
            if (existing == null) return false;

            _mapper.Map(brand, existing);
            _brandRepo.Update(existing);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var item = _brandRepo.GetById(id);
            if (item == null) return false;

            item.IsDeleted = true;
            _brandRepo.Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        #endregion
    }
}
