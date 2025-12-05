using StayZee.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StayZee.Application.Interfaces.Iservices
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task BlockUserAsync(int userId);
        Task UnblockUserAsync(int userId);
        Task<IEnumerable<Home>> GetPendingHomesAsync();
        Task ApproveHomeAsync(Guid homeId);
        Task RejectHomeAsync(Guid homeId);
    }
}
