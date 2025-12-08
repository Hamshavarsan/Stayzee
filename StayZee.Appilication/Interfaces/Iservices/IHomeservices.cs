using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;


namespace StayZee.Application.Interfaces.Iservices
{
    public interface IHomeService
    {
        Task<HomeResponseDto> CreateHomeAsync(HomeRequestDto request);
        Task<HomeResponseDto?> GetHomeByIdAsync(Guid id);
        Task<IEnumerable<HomeResponseDto>> GetAllHomesAsync();
    }
}
