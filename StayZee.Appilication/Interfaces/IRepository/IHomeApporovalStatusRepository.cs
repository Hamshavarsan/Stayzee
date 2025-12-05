using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayZee.Domain.Entities;

namespace StayZee.Appilication.Interfaces.IRepository
{
    public interface IHomeApporovalStatusRepository
    {
        Task<HomeApprovalStatus?> GetByNameAsync(string name);
    }
}
