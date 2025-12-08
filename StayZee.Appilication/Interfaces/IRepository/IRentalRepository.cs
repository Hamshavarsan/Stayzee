using StayZee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayZee.Appilication.Interfaces.IRepository
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllAsync();
    }
}
