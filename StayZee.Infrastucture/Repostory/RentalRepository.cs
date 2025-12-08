using Microsoft.EntityFrameworkCore;
using StayZee.Appilication.Interfaces.IRepository;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Infrastructure.Repostory
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals.ToListAsync();
        }
    }
}
