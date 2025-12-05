using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayZee.Domain.Entities;


namespace StayZee.Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
    }
}
