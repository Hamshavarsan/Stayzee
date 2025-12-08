using Microsoft.EntityFrameworkCore;
using StayZee.Application.Interfaces;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;

namespace StayZee.Infrastructure.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDbContext _context;

        public HomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Home home)
        {
            await _context.Homes.AddAsync(home);
            await _context.SaveChangesAsync();
        }

        public async Task<Home?> GetByIdAsync(Guid id)
        {
            return await _context.Homes
                .Include(h => h.HomeApprovalStatus)
                .Include(h => h.Bookings)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Home>> GetAllAsync()
        {
            return await _context.Homes
                .Include(h => h.HomeApprovalStatus)
                .Include(h => h.Bookings)
                .ToListAsync();
        }

        public async Task UpdateAsync(Home home)
        {
            _context.Homes.Update(home);
            await _context.SaveChangesAsync();
        }
    }
}
