using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;


namespace StayZee.Application.Interfaces.Iservices
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request);
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync();
        Task<BookingResponseDto?> GetBookingByIdAsync(Guid bookingId);
    }
}
