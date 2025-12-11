using Microsoft.EntityFrameworkCore;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StayZee.Infrastructure.Repository
{
    //public class BookingRepository : IBookingRepository
    //{
    //    private readonly AppDbContext _context;

    //    public BookingRepository(AppDbContext context) => _context = context;

    //public async Task AddAsync(Booking booking)
    //{
    //    await _context.Bookings.AddAsync(booking);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task<Booking?> GetByIdAsync(Guid bookingId)
    //{
    //    return await _context.Bookings
    //        .Include(b => b.Home)
    //        .Include(b => b.BookingStatus)
    //        .Include(b => b.PaymentStatus)
    //        .Include(b => b.SharedCustomers)
    //            .ThenInclude(sc => sc.Customer)
    //        .FirstOrDefaultAsync(b => b.BookingId == bookingId);
    //}

    //public async Task<IEnumerable<Booking>> GetAllAsync()
    //{
    //    return await _context.Bookings
    //        .Include(b => b.Home)
    //        .Include(b => b.BookingStatus)
    //        .Include(b => b.PaymentStatus)
    //        .Include(b => b.SharedCustomers)
    //            .ThenInclude(sc => sc.Customer)
    //        .ToListAsync();
    //}

    //public async Task UpdateAsync(Booking booking)
    //{
    //    _context.Bookings.Update(booking);
    //    await _context.SaveChangesAsync();
    //}

    //// NEW: add shared customers (batch)
    //public async Task AddSharedCustomersAsync(IEnumerable<BookingSharedCustomer> shared)
    //{
    //    await _context.BookingSharedCustomers.AddRangeAsync(shared);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task<IEnumerable<BookingSharedCustomer>> GetSharedCustomersByBookingAsync(Guid bookingId)
    //{
    //    return await _context.BookingSharedCustomers
    //        .Where(x => x.BookingId == bookingId)
    //        .Include(x => x.Customer)
    //        .ToListAsync();
    //}

    //public async Task<int> CountSharedCustomersAsync(Guid bookingId)
    //{
    //    return await _context.BookingSharedCustomers.CountAsync(x => x.BookingId == bookingId);
    //}
//}
}
