using Microsoft.EntityFrameworkCore;

using StayZee.Application.Interfaces.IRepository;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;

namespace StayZee.Infrastructure.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Home)
                .Include(b => b.BookingStatus)
                .Include(b => b.PaymentStatus)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Home)
                .Include(b => b.BookingStatus)
                .Include(b => b.PaymentStatus)
                .ToListAsync();
        }
    }

   
}
