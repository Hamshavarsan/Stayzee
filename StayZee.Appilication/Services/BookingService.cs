using Microsoft.EntityFrameworkCore;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Application.Interfaces.IRepository;

using StayZee.Domain.Entities;
using StayZee.Application.Interfaces;

namespace StayZee.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IHomeRepository _homeRepo;
       

        public BookingService(
            IBookingRepository bookingRepo,
            IHomeRepository homeRepo
            )
        {
            _bookingRepo = bookingRepo;
            _homeRepo = homeRepo;
            
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request)
        {
            // Load related data
            var home = await _homeRepo.GetByIdAsync(request.HomeId);
            if (home == null) throw new Exception("Home not found");

            

            var booking = new Booking
            {
                CustomerId = request.CustomerId,
                HomeId = request.HomeId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                TotalPrice = request.TotalPrice,
                BookingStatusId = request.BookingStatusId,
               
            };

            await _bookingRepo.AddAsync(booking);

            return new BookingResponseDto
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                HomeId = booking.HomeId,
                HomeName = home.Name,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice,
                
                CreatedAt = booking.CreatedAt
            };
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();

            return bookings.Select(b => new BookingResponseDto
            {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                HomeId = b.HomeId,
                HomeName = b.Home?.Name,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice,
                
                CreatedAt = b.CreatedAt
            });
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(Guid bookingId)
        {
            var b = await _bookingRepo.GetByIdAsync(bookingId);
            if (b == null) return null;

            return new BookingResponseDto
            {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                HomeId = b.HomeId,
                HomeName = b.Home?.Name,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice,
                BookingStatus = b.BookingStatus?.BookingStatusName,
              
                CreatedAt = b.CreatedAt
            };
        }
    }
}
