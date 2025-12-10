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
        private const int MAX_SHARED_PER_BOOKING = 10;

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
                Status = "Created"
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
                CreatedAt = b.CreatedAt,
                SharedCustomerIds = b.SharedCustomers?.Select(s => s.CustomerId).ToList(),
                SharedCustomerEmails = b.SharedCustomers?.Select(s => s.Customer?.Email).ToList()
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
                CreatedAt = b.CreatedAt,
                SharedCustomerIds = b.SharedCustomers?.Select(s => s.CustomerId).ToList(),
                SharedCustomerEmails = b.SharedCustomers?.Select(s => s.Customer?.Email).ToList()
            };
        }

        // -------------------- Share Booking --------------------
        public async Task<BookingResponseDto> ShareBookingAsync(BookingShareRequestDto request)
        {
            var booking = await _bookingRepo.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking not found");

            if (request.Emails == null || !request.Emails.Any())
                throw new Exception("You must provide at least one email address");

            // remove duplicates and trim
            var candidateEmails = request.Emails
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => e.Trim().ToLowerInvariant())
                .Distinct()
                .ToList();

            if (!candidateEmails.Any())
                throw new Exception("No valid email addresses provided");

            // how many already shared?
            var alreadyCount = await _bookingRepo.CountSharedCustomersAsync(booking.BookingId);
            var remainingSlots = MAX_SHARED_PER_BOOKING - alreadyCount;
            if (remainingSlots <= 0)
                throw new Exception($"Booking already has maximum of {MAX_SHARED_PER_BOOKING} shared users.");

            var validShared = new List<BookingSharedCustomer>();

            foreach (var email in candidateEmails)
            {
                if (validShared.Count >= remainingSlots) break;

                var customer = await _customerRepo.GetByEmailAsync(email);
                if (customer == null) continue; // skip unregistered emails

                // don't duplicate if already shared
                var alreadyShared = booking.SharedCustomers.Any(sc => sc.CustomerId == customer.Id);
                if (alreadyShared) continue;

                validShared.Add(new BookingSharedCustomer
                {
                    BookingId = booking.BookingId,
                    CustomerId = customer.Id
                });
            }

            if (!validShared.Any())
                throw new Exception("No valid registered users found or all provided users already added.");

            // persist shared users
            await _bookingRepo.AddSharedCustomersAsync(validShared);

            // refresh booking with shared customers
            booking = await _bookingRepo.GetByIdAsync(booking.BookingId);

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
                CreatedAt = booking.CreatedAt,
                SharedCustomerIds = booking.SharedCustomers?.Select(s => s.CustomerId).ToList(),
                SharedCustomerEmails = booking.SharedCustomers?.Select(s => s.Customer?.Email).ToList()
            };
        }
    }
}
