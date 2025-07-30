using AutoMapper;
using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Blogs> _blogsRepo;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blogsRepo = _unitOfWork.GetRepository<Blogs>();
        }

        #region Async Methods

        public async Task<IEnumerable<BlogsResponse>> GetAllAsync()
        {
            var result = await _blogsRepo.GetAllAsync(x => !x.IsDeleted);
            var item = _mapper.Map<IEnumerable<BlogsResponse>>(result);
            return item;
        }

        public async Task<BlogsResponse?> GetByIdAsync(string id)
        {
            var result = await _blogsRepo.GetOneAsync(x => x.Id == id && !x.IsDeleted);
            var response = _mapper.Map<BlogsResponse>(result);
            return response;
        }
        public async Task<IEnumerable<BlogsResponse>> GetByBlogTypeIdAsync(string idBlogType)
        {
            var result = await _blogsRepo.GetAllAsync(x => x.BlogTypeId == idBlogType && !x.IsDeleted);
            var response = _mapper.Map<IEnumerable<BlogsResponse>>(result);
            return response;
        }
        public async Task<BasePaginatedList<BlogsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var result = _blogsRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = await _blogsRepo.GetPaggingAsync(result, pageNumber, pageSize);
            var response = _mapper.Map<List<BlogsResponse>>(pagingResult.Items);
            return new BasePaginatedList<BlogsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<BlogsResponse>> SearchAsync(string? keyword)
        {
            var result = _blogsRepo
                .Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.Contains(keyword)
                    || (x.BlogType != null && x.BlogType.Name.Contains(keyword))
                    || (x.Author != null && x.Author.FullName.Contains(keyword))
                    || x.Status.Contains(keyword)
                )).AsEnumerable();
            var response = _mapper.Map<IEnumerable<BlogsResponse>>(result);
            return await Task.FromResult(response);
        }

        public async Task<bool> CreateAsync(BlogsRequest blog)
        {
            var resquest = _mapper.Map<Blogs>(blog);
            await _blogsRepo.CreateAsync(resquest);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _blogsRepo.GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            await _blogsRepo.UpdateAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(BlogsRequest blog)
        {
            var exitItem = await _blogsRepo.GetOneAsync(x => x.Id == blog.Id && !x.IsDeleted);
            if (exitItem == null)
            {
                return false;
            }
            _mapper.Map(blog, exitItem);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<BlogsResponse> GetAll()
        {
            var result = _blogsRepo.GetAll(x => !x.IsDeleted);
            var response = _mapper.Map<IEnumerable<BlogsResponse>>(result);
            return response;
        }

        public BlogsResponse? GetById(string id)
        {
            var result = _blogsRepo.GetOne(x => x.Id == id && !x.IsDeleted);
            var response = _mapper.Map<BlogsResponse>(result);
            return response;
        }

        public IEnumerable<BlogsResponse> Search(string? keyword)
        {
            var result = _blogsRepo
                .Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.Contains(keyword)
                    || x.BlogType.Name.Contains(keyword)
                    || x.Author.FullName.Contains(keyword)
                    || x.Status.Contains(keyword)
                )).AsEnumerable();
            var response = _mapper.Map<IEnumerable<BlogsResponse>>(result);
            return response;
        }

        public bool Create(BlogsRequest blog)
        {
            var request = _mapper.Map<Blogs>(blog);
            _blogsRepo.Create(request);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(BlogsRequest blog)
        {
            var exitItem = _blogsRepo.GetOne(x => x.Id == blog.Id && !x.IsDeleted);
            if (exitItem == null)
            {
                return false;
            }
            _mapper.Map(blog, exitItem);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var item = _blogsRepo.GetById(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            _blogsRepo.Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public BasePaginatedList<BlogsResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var result = _blogsRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = _blogsRepo.GetPagging(result, pageNumber, pageSize);
            var response = _mapper.Map<List<BlogsResponse>>(pagingResult.Items);
            return new BasePaginatedList<BlogsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        #endregion
    }
}
