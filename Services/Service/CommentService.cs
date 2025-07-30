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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Comments> _commentRepo;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commentRepo = _unitOfWork.GetRepository<Comments>();
        }

        #region Async Methods

        public async Task<IEnumerable<CommentsResponse>> GetAllAsync()
        {
            var result = await _commentRepo.GetAllAsync(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<CommentsResponse>>(result);
        }

        public async Task<CommentsResponse?> GetByIdAsync(string id)
        {
            var result = await _commentRepo.GetOneAsync(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<CommentsResponse>(result);
        }

        public async Task<BasePaginatedList<CommentsResponse>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var query = _commentRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = await _commentRepo.GetPaggingAsync(query, pageNumber, pageSize);
            var response = _mapper.Map<List<CommentsResponse>>(pagingResult.Items);
            return new BasePaginatedList<CommentsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public async Task<IEnumerable<CommentsResponse>> SearchAsync(string? keyword)
        {
            var result = _commentRepo.Entities
                .Include(x => x.User)
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.User.FullName.Contains(keyword)
                )).AsEnumerable();

            return await Task.FromResult(_mapper.Map<IEnumerable<CommentsResponse>>(result));
        }

        public async Task<bool> CreateAsync(CommentsRequest comment)
        {
            var entity = _mapper.Map<Comments>(comment);
            await _commentRepo.CreateAsync(entity);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CommentsRequest comment)
        {
            var existing = await _commentRepo.GetOneAsync(x => x.Id == comment.Id && !x.IsDeleted);
            if (existing == null) return false;

            _mapper.Map(comment, existing);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _commentRepo.GetByIdAsync(id);
            if (item == null) return false;

            item.IsDeleted = true;
            await _commentRepo.UpdateAsync(item);
            return await _unitOfWork.SaveAsync() > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<CommentsResponse> GetAll()
        {
            var result = _commentRepo.GetAll(x => !x.IsDeleted);
            return _mapper.Map<IEnumerable<CommentsResponse>>(result);
        }

        public CommentsResponse? GetById(string id)
        {
            var result = _commentRepo.GetOne(x => x.Id == id && !x.IsDeleted);
            return _mapper.Map<CommentsResponse>(result);
        }

        public BasePaginatedList<CommentsResponse> GetPaginatedList(int pageNumber, int pageSize)
        {
            var query = _commentRepo.Entities.Where(x => !x.IsDeleted);
            var pagingResult = _commentRepo.GetPagging(query, pageNumber, pageSize);
            var response = _mapper.Map<List<CommentsResponse>>(pagingResult.Items);
            return new BasePaginatedList<CommentsResponse>(response, pagingResult.TotalItems, pageNumber, pageSize);
        }

        public IEnumerable<CommentsResponse> Search(string? keyword)
        {
            var result = _commentRepo.Entities
                .Where(x => !x.IsDeleted && (
                    string.IsNullOrEmpty(keyword)
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.User.FullName.Contains(keyword)
                )).AsEnumerable();

            return _mapper.Map<IEnumerable<CommentsResponse>>(result);
        }

        public bool Create(CommentsRequest comment)
        {
            var entity = _mapper.Map<Comments>(comment);
            _commentRepo.Create(entity);
            return _unitOfWork.Save() > 0;
        }

        public bool Update(CommentsRequest comment)
        {
            var existing = _commentRepo.GetOne(x => x.Id == comment.Id && !x.IsDeleted);
            if (existing == null) return false;

            _mapper.Map(comment, existing);
            return _unitOfWork.Save() > 0;
        }

        public bool Delete(string id)
        {
            var item = _commentRepo.GetById(id);
            if (item == null) return false;

            item.IsDeleted = true;
            _commentRepo.Update(item);
            return _unitOfWork.Save() > 0;
        }

        #endregion
    }
}
