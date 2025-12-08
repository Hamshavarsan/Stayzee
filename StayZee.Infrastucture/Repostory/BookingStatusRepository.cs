using Microsoft.EntityFrameworkCore;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StayZee.Infrastructure.Repository
{
    public class BookingStatusRepository : IBookingStatusRepository
    {
        // Only ONE _context
        private readonly AppDbContext _context;

        public BookingStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookingStatus?> GetByIdAsync(Guid id)
        {
            return await _context.BookingStatuses
                .FirstOrDefaultAsync(b => b.BookingStatusId == id);
        }

        public async Task<BookingStatus?> GetByNameAsync(string name)
        {
            return await _context.BookingStatuses
                .FirstOrDefaultAsync(b => b.BookingStatusName == name);
        }

        public async Task<IEnumerable<BookingStatus>> GetAllAsync()
        {
            return await _context.BookingStatuses.ToListAsync();
        }

        public async Task AddAsync(BookingStatus bookingStatus)
        {
            await _context.BookingStatuses.AddAsync(bookingStatus);
            await _context.SaveChangesAsync();
        }
    }
}
