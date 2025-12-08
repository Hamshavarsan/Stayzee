using Microsoft.EntityFrameworkCore;

using StayZee.Appilication.Interfaces.IRepository;

using StayZee.Application.Interfaces.IRepository;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StayZee.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }


        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }


        public async Task AddAsync(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

    }
}
