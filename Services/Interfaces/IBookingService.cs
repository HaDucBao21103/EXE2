using BusinessObject;
using Core.Base;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        #region Non-Async Methods

        IEnumerable<Bookings> GetAll();
        Bookings? GetById(string id);
        BasePaginatedList<Bookings> GetPaginatedList(int pageNumber, int pageSize);
        IEnumerable<Bookings> Search(string keyword);
        bool Create(Bookings booking);
        bool Update(Bookings booking);
        bool Delete(string id);

        #endregion

        #region Async Methods

        Task<IEnumerable<Bookings>> GetAllAsync();
        Task<Bookings?> GetByIdAsync(string id);
        Task<BasePaginatedList<Bookings>> GetPaginatedListAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Bookings>> SearchAsync(string keyword);
        Task<bool> CreateAsync(Bookings booking);
        Task<bool> UpdateAsync(Bookings booking);
        Task<bool> DeleteAsync(string id);

        #endregion
    }
}
