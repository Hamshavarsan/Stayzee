using StayZee.Domain.Entities;

namespace StayZee.Application.Interfaces.IRepository
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task<Booking?> GetByIdAsync(Guid bookingId);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task UpdateAsync(Booking booking);
    }

}

    
