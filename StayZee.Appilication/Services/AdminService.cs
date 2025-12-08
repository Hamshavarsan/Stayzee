using StayZee.Application.Interfaces;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Appilication.Interfaces.IRepository;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StayZee.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly IHomeApporovalStatusRepository _approvalStatusRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;

        public AdminService(
            IUserRepository userRepository, 
            IHomeRepository homeRepository, 
            IHomeApporovalStatusRepository approvalStatusRepository,
            ICustomerRepository customerRepository,
            IRentalRepository rentalRepository)
        {
            _userRepository = userRepository;
            _homeRepository = homeRepository;
            _approvalStatusRepository = approvalStatusRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task BlockUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = true;
                await _userRepository.UpdateUserAsync(user);
            }
        }

        public async Task UnblockUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = false;
                await _userRepository.UpdateUserAsync(user);
            }
        }

        public async Task<IEnumerable<Home>> GetPendingHomesAsync()
        {
            var homes = await _homeRepository.GetAllAsync();
            return homes.Where(h => h.HomeApprovalStatus?.Name == "Pending");
        }

        public async Task ApproveHomeAsync(Guid homeId)
        {
            var home = await _homeRepository.GetByIdAsync(homeId);
            if (home != null)
            {
                var status = await _approvalStatusRepository.GetByNameAsync("Approved");
                if (status != null)
                {
                    home.HomeApprovalStatusId = status.HomeApprovalStatusId;
                    home.HomeApprovalStatus = status;
                    await _homeRepository.UpdateAsync(home);
                }
            }
        }

        public async Task RejectHomeAsync(Guid homeId)
        {
            var home = await _homeRepository.GetByIdAsync(homeId);
            if (home != null)
            {
                var status = await _approvalStatusRepository.GetByNameAsync("Rejected");
                if (status != null)
                {
                    home.HomeApprovalStatusId = status.HomeApprovalStatusId;
                    home.HomeApprovalStatus = status;
                    await _homeRepository.UpdateAsync(home);
                }
            }
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _rentalRepository.GetAllAsync();
        }
    }
}
