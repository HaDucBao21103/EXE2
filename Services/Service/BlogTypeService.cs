using AutoMapper;
using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class BlogTypeService : IBlogTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BlogTypes> _blogTypeRepo;

        public BlogTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blogTypeRepo = _unitOfWork.GetRepository<BlogTypes>();
        }

        #region Async Methods

        public async Task<IEnumerable<BlogTypesResponse>> GetAllAsync()
        {
            var result = await _blogTypeRepo.GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<BlogTypesResponse>>(result);
        }

        public async Task<BlogTypesResponse?> GetByIdAsync(string id)
        {
            var result = await _blogTypeRepo.GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<BlogTypesResponse>(result);
        }

        public async Task<BasePaginatedList<BlogTypesResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var query = _blogTypeRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = await _blogTypeRepo.GetPaggingAsync(query, pageNumber, pageSize);
            var response = _mapper.Map<List<BlogTypesResponse>>(pagingResult.Items);
            return new BasePaginatedList<BlogTypesResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<BlogTypesResponse>> SearchAsync(string keyword)
        {
            var result = _blogTypeRepo.Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Name.Contains(keyword)
                    || x.Description.Contains(keyword)))
                .AsEnumerable();

            var response = _mapper.Map<IEnumerable<BlogTypesResponse>>(result);
            return await Task.FromResult(response);
        }

        public async Task<bool> CreateAsync(BlogTypesRequest blogType)
        {
            var entity = _mapper.Map<BlogTypes>(blogType);
            await _blogTypeRepo.CreateAsync(entity);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(BlogTypesRequest blogType)
        {
            var existing = await _blogTypeRepo.GetOneAsync(x => x.Id == blogType.Id && !x.IsDeleted);
            if (existing == null) return false;

            _mapper.Map(blogType, existing);
            await _blogTypeRepo.UpdateAsync(existing);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _blogTypeRepo.GetByIdAsync(id);
            if (item == null) return false;

            item.IsDeleted = true;
            await _blogTypeRepo.UpdateAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<BlogTypesResponse> GetAll()
        {
            var result = _blogTypeRepo.GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<BlogTypesResponse>>(result);
        }

        public BlogTypesResponse? GetById(string id)
        {
            var result = _blogTypeRepo.GetOne(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<BlogTypesResponse>(result);
        }

        public BasePaginatedList<BlogTypesResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var query = _blogTypeRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = _blogTypeRepo.GetPagging(query, pageNumber, pageSize);
            var response = _mapper.Map<List<BlogTypesResponse>>(pagingResult.Items);
            return new BasePaginatedList<BlogTypesResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public IEnumerable<BlogTypesResponse> Search(string keyword)
        {
            var result = _blogTypeRepo.Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Name.Contains(keyword)
                    || x.Description.Contains(keyword)))
                .AsEnumerable();

            return _mapper.Map<IEnumerable<BlogTypesResponse>>(result);
        }

        public bool Create(BlogTypesRequest blogType)
        {
            var entity = _mapper.Map<BlogTypes>(blogType);
            _blogTypeRepo.Create(entity);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(BlogTypesRequest blogType)
        {
            var existing = _blogTypeRepo.GetOne(x => x.Id == blogType.Id && !x.IsDeleted);
            if (existing == null) return false;

            _mapper.Map(blogType, existing);
            _blogTypeRepo.Update(existing);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var item = _blogTypeRepo.GetById(id);
            if (item == null) return false;

            item.IsDeleted = true;
            _blogTypeRepo.Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        #endregion
    }
}
