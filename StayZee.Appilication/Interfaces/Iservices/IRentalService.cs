using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.DTOs.ResponseDTO.StayZee.Application.DTOs.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StayZee.Application.Interfaces.Iservices
{
    public interface IRentalService
    {
        Task<RentalResponseDTO> CreateRental(CreateRentalRequest request);
        Task<List<RentalDto>> GetAllRentalsAsync(); // parameter இல்லை!
    
        Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequest request);
        Task<RentalResponseDTO?> GetRentalByIdAsync(int id);

    }
}
