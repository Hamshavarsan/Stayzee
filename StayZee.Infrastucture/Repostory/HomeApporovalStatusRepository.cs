using StayZee.Appilication.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;

namespace StayZee.Infrastructure.Repository
{
    public class HomeApporovalStatusRepository : IHomeApporovalStatusRepository
    {
        private readonly AppDbContext _context;

        public HomeApporovalStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HomeApprovalStatus?> GetByNameAsync(string name)
        {
            return await _context.HomeApprovalStatuses
                .FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
