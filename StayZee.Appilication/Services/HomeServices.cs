using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Domain.Entities;

namespace StayZee.Application.Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepo;

        public HomeService(IHomeRepository homeRepo)
        {
            _homeRepo = homeRepo;
        }

        public async Task<HomeResponseDto> CreateHomeAsync(HomeRequestDto request)
        {
            var home = new Home
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                ImagePath = request.ImagePath,
                AvailableFrom = request.AvailableFrom,
                AvailableTo = request.AvailableTo,
                Features = request.Features,
                RatePerDay = request.RatePerDay,
                OwnerId = request.OwnerId,
                HomeApprovalStatusId = request.HomeApprovalStatusId
            };

            await _homeRepo.AddAsync(home);

            return new HomeResponseDto
            {
                Id = home.Id,
                Name = home.Name,
                Description = home.Description,
                Address = home.Address,
                ImagePath = home.ImagePath,
                AvailableFrom = home.AvailableFrom,
                AvailableTo = home.AvailableTo,
                Features = home.Features,
                RatePerDay = home.RatePerDay,
                OwnerId = home.OwnerId,
                ApprovalStatus = home.HomeApprovalStatus?.Name
            };
        }

        public async Task<HomeResponseDto?> GetHomeByIdAsync(Guid id)
        {
            var home = await _homeRepo.GetByIdAsync(id);
            if (home == null) return null;

            return new HomeResponseDto
            {
                Id = home.Id,
                Name = home.Name,
                Description = home.Description,
                Address = home.Address,
                ImagePath = home.ImagePath,
                AvailableFrom = home.AvailableFrom,
                AvailableTo = home.AvailableTo,
                Features = home.Features,
                RatePerDay = home.RatePerDay,
                OwnerId = home.OwnerId,
                ApprovalStatus = home.HomeApprovalStatus?.Name
            };
        }

        public async Task<IEnumerable<HomeResponseDto>> GetAllHomesAsync()
        {
            var homes = await _homeRepo.GetAllAsync();

            return homes.Select(home => new HomeResponseDto
            {
                Id = home.Id,
                Name = home.Name,
                Description = home.Description,
                Address = home.Address,
                ImagePath = home.ImagePath,
                AvailableFrom = home.AvailableFrom,
                AvailableTo = home.AvailableTo,
                Features = home.Features,
                RatePerDay = home.RatePerDay,
                OwnerId = home.OwnerId,
                ApprovalStatus = home.HomeApprovalStatus?.Name
            });
        }
    }
}
