using StayZee.Domain.Entities;

namespace StayZee.Application.Interfaces
{
    public interface IHomeRepository
    {
        Task AddAsync(Home home);
        Task<Home?> GetByIdAsync(Guid id);
        Task<IEnumerable<Home>> GetAllAsync();
        Task UpdateAsync(Home home);
    }
}
