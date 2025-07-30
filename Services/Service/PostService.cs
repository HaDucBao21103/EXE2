using AutoMapper;
using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Reflection.Metadata;
using ViewModels.Request;
using ViewModels.Response;

namespace Services.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Post> _postsRepo;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _postsRepo = _unitOfWork.GetRepository<Post>();
        }

        #region Async Methods

        public async Task<IEnumerable<PostsResponse>> GetAllAsync()
        {
            var result = await _postsRepo.GetAllAsync(x => !x.IsDeleted);
            var item = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return item;
        }

        public async Task<PostsResponse?> GetByIdAsync(string id)
        {
            var result = await _postsRepo.GetOneAsync(x => x.Id == id && !x.IsDeleted);
            var response = _mapper.Map<PostsResponse>(result);
            return response;
        }
        public async Task<BasePaginatedList<PostsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var result = _postsRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = await _postsRepo.GetPaggingAsync(result, pageNumber, pageSize);
            var response = _mapper.Map<List<PostsResponse>>(pagingResult.Items);
            return new BasePaginatedList<PostsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<PostsResponse>> SearchAsync(string? keyword)
        {
            var result = _postsRepo
                .Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.Contains(keyword)
                    || (x.Author != null && x.Author.FullName.Contains(keyword))
                    || x.Status.Contains(keyword)
                )).AsEnumerable();
            var response = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return await Task.FromResult(response);
        }

        public async Task<bool> CreateAsync(PostsCreateRequest blog)
        {
            var resquest = _mapper.Map<Post>(blog);
            await _postsRepo.CreateAsync(resquest);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _postsRepo.GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            await _postsRepo.UpdateAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(PostsRequest blog)
        {
            var exitItem = await _postsRepo.GetOneAsync(x => x.Id == blog.Id && !x.IsDeleted);
            if (exitItem == null)
            {
                return false;
            }
            _mapper.Map(blog, exitItem);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateStatusAsync(string status, string id)
        {
            var exitItem = await _postsRepo.GetOneAsync(x => x.Id == id.ToString() && !x.IsDeleted);
            if (exitItem == null)
            {
                return false;
            }
            exitItem.Status = status;
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<PostsResponse> GetAll()
        {
            var result = _postsRepo.GetAll(x => !x.IsDeleted);
            var response = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return response;
        }

        public PostsResponse? GetById(string id)
        {
            var result = _postsRepo.GetOne(x => x.Id == id && !x.IsDeleted);
            var response = _mapper.Map<PostsResponse>(result);
            return response;
        }

        public IEnumerable<PostsResponse> Search(string? keyword)
        {
            var result = _postsRepo
                .Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.Contains(keyword)
                    || x.Author.FullName.Contains(keyword)
                    || x.Status.Contains(keyword)
                )).AsEnumerable();
            var response = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return response;
        }

        public bool Create(PostsCreateRequest blog)
        {
            var request = _mapper.Map<Post>(blog);
            _postsRepo.Create(request);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(PostsRequest blog)
        {
            var exitItem = _postsRepo.GetOne(x => x.Id == blog.Id && !x.IsDeleted);
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
            var item = _postsRepo.GetById(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            _postsRepo.Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public BasePaginatedList<PostsResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var result = _postsRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = _postsRepo.GetPagging(result, pageNumber, pageSize);
            var response = _mapper.Map<List<PostsResponse>>(pagingResult.Items);
            return new BasePaginatedList<PostsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }


        #endregion
    }
}
