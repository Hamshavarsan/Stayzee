using StayZee.Appilication.Interfaces.IRepository;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StayZee.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IHomeRepository _homeRepo;
        private readonly ICustomerRepository _customerRepo;

        public BookingService(
            IBookingRepository bookingRepo,
            IHomeRepository homeRepo,
            ICustomerRepository customerRepo
        )
        {
            _bookingRepo = bookingRepo;
            _homeRepo = homeRepo;
            _customerRepo = customerRepo;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request)
        {
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
                Status = "Created" // ✅ Initialize non-nullable property
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

        // -------------------- Share Booking --------------------
        public async Task<BookingResponseDto> ShareBookingAsync(BookingShareRequestDto request)
        {
            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking not found");

            if (request.Emails == null || request.Emails.Count == 0)
                throw new Exception("You must provide at least one email address");

            var validEmails = new List<string>();

            foreach (var email in request.Emails)
            {
                var customer = await _customerRepo.GetByEmailAsync(email);
                if (customer != null) // ✅ Compare against object, not void
                    validEmails.Add(email);
            }

            if (!validEmails.Any())
                throw new Exception("No valid registered emails provided");

            booking.SharedEmails = string.Join(",", validEmails);
            booking.SharedAt = DateTime.UtcNow;

            // ✅ DO NOT assign void
            await _bookingRepo.UpdateAsync(booking);

            return new BookingResponseDto
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                HomeId = booking.HomeId,
                HomeName = booking.Home?.Name,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice,
                BookingStatus = booking.BookingStatus?.BookingStatusName,
                CreatedAt = booking.CreatedAt
            };
        }
    }
}
    

