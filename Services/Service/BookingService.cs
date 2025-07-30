using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Service
{
    public class BookingService : IBookingService
    {
        public readonly IUnitOfWork _unitOfWork;
        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region Async Methods

        public async Task<IEnumerable<Bookings>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<Bookings>().GetAllAsync(x => !x.IsDeleted);
        }

        public async Task<Bookings?> GetByIdAsync(string id)
        {

            return await _unitOfWork.GetRepository<Bookings>().GetOneAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<BasePaginatedList<Bookings>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var items = await _unitOfWork.GetRepository<Bookings>().GetAllAsync(x => !x.IsDeleted);
            return new BasePaginatedList<Bookings>(items.ToList(), items.Count(), pageNumber, pageSize);
        }

        public async Task<IEnumerable<Bookings>> SearchAsync(string keyword)
        {
            var result = _unitOfWork.GetRepository<Bookings>()
                .Entities
                .Where(x =>
                !x.IsDeleted
                && (x.Location.ToLower().Contains(keyword.ToLower()) || string.IsNullOrEmpty(keyword))
                || x.Status.Contains(keyword) || string.IsNullOrEmpty(keyword)
                ).AsEnumerable();
            return await Task.FromResult(result.ToList());
        }

        public async Task<bool> CreateAsync(Bookings booking)
        {
            await _unitOfWork.GetRepository<Bookings>().CreateAsync(booking);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _unitOfWork.GetRepository<Bookings>().GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            await _unitOfWork.GetRepository<Bookings>().DeleteAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Bookings booking)
        {
            await _unitOfWork.GetRepository<Bookings>().UpdateAsync(booking);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        #endregion

        #region Non-Async Methods

        public IEnumerable<Bookings> GetAll()
        {
            return _unitOfWork.GetRepository<Bookings>().GetAll(x => !x.IsDeleted);
        }

        public Bookings? GetById(string id)
        {
            return _unitOfWork.GetRepository<Bookings>().GetOne(x => x.Id == id && !x.IsDeleted);
        }

        public IEnumerable<Bookings> Search(string keyword)
        {
            var result = _unitOfWork.GetRepository<Bookings>()
                .Entities
                .Where(x =>
                !x.IsDeleted
                && (x.Location.Contains(keyword) || string.IsNullOrEmpty(keyword))
                || x.Status.Contains(keyword) || string.IsNullOrEmpty(keyword)
                ).AsEnumerable();
            return result.ToList();
        }

        public bool Create(Bookings booking)
        {
            _unitOfWork.GetRepository<Bookings>().Create(booking);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(Bookings booking)
        {
            _unitOfWork.GetRepository<Bookings>().Update(booking);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var item = _unitOfWork.GetRepository<Bookings>().GetById(id);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            _unitOfWork.GetRepository<Bookings>().Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public BasePaginatedList<Bookings> GetPaginatedList(int pageNumber, int pageSize)
        {
            var items = _unitOfWork.GetRepository<Bookings>().GetAll(x => !x.IsDeleted);
            return new BasePaginatedList<Bookings>(items.ToList(), items.Count(), pageNumber, pageSize);
        }

        #endregion
    }
}
