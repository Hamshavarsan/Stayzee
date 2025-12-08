using StayZee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StayZee.Application.Interfaces.IRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer?> GetByEmailAsync(string email);
        Task AddAsync(Customer customer);

    }
}
