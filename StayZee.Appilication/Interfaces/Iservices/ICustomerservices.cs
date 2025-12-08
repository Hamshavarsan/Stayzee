using StayZee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Application.Interfaces.Iservices
{
    public interface ICustomerservices
    {
        Task<Customer?> GetByEmailAsync(string email);
    }
}
