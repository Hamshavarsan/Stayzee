using Microsoft.EntityFrameworkCore;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.DTOs.ResponseDTO.StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;

namespace StayZee.Infrastructure.Repository
{
    public class RentalService : IRentalService
    {
        private readonly AppDbContext _context;
        private readonly ICloudService _cloud;

        public RentalService(AppDbContext context, ICloudService cloud)
        {
            _context = context;
            _cloud = cloud;
        }

        public Task<BookingResponseDto> CreateBookingAsync(Guid rentalId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<RentalResponse> CreateRental(CreateRentalRequest request)
        {
            if (request.Photos == null || request.Photos.Count < 4)
                throw new Exception("Minimum 4 photos required");

            var urls = await _cloud.UploadImagesAsync(request.Photos);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // User எடுத்து Role மாற்று → Database-லே "Rentals" ஆக மாறும்
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (user == null)
                    throw new Exception("User not found");

                user.Role = "Rentals"; // இதுதான் நீ வேண்டிய முக்கியமான line

                var rental = new Rental
                {
                    UserId = request.UserId,
                    HomeTitle = request.HomeTitle,
                    HomeLocation = request.HomeLocation,
                    Bedrooms = request.Bedrooms,
                    PetFriendly = request.PetFriendly,
                    OneDayPrice = request.OneDayPrice,
                    MonthPrice = request.MonthPrice,
                    CurrentBill = request.CurrentBill,
                    PhotoUrl1 = urls[0],
                    PhotoUrl2 = urls[1],
                    PhotoUrl3 = urls[2],
                    PhotoUrl4 = urls[3]
                   
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();     // User.Role + Rental → இரண்டும் save ஆகும்
                await transaction.CommitAsync();

                return new RentalResponse
                {
                    RentalId = rental.Id,
                   // Message = "Property listed successfully! Role updated to Rentals"
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        // StayZee.Infrastructure.Repository → RentalService.cs

        // GET ALL RENTALS – HOME PAGE-க்கு இதுதான் தேவை
        public async Task<List<RentalDto>> GetAllRentalsAsync()
        {
            var rentals = await _context.Rentals
                //.Where(r => r.IsApproved == true && r.IsDeleted != true) // நல்ல வீடுகள் மட்டும்
                .Select(r => new RentalDto
                {
                    Id = r.Id,
                    HomeTitle = r.HomeTitle ?? "Luxury Stay",
                    HomeLocation = r.HomeLocation ?? "Sri Lanka",
                    Bedrooms = r.Bedrooms,
                    PetFriendly = r.PetFriendly,
                    OneDayPrice = r.OneDayPrice,
                    MonthPrice = r.MonthPrice,
                    PhotoUrls = new List<string>
                    {
                        r.PhotoUrl1 ?? "",
                        r.PhotoUrl2 ?? "",
                        r.PhotoUrl3 ?? "",
                        r.PhotoUrl4 ?? ""
                    }
                    .Where(url => !string.IsNullOrWhiteSpace(url))
                    .Take(4) // max 4 photos
                    .ToList()
                })
                .ToListAsync();

            return rentals;
        }
        public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingRequest request)
        {
            if (request.StartDate >= request.EndDate)
                throw new Exception("End date must be after start date");

            var rental = await _context.Rentals
                .FirstOrDefaultAsync(r => r.Id == request.RentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            // Check overlapping bookings
            var isOverlapping = await _context.Bookings.AnyAsync(b =>
                b.RentalId == request.RentalId &&
                b.StartDate < request.EndDate &&
                b.EndDate > request.StartDate);

            if (isOverlapping)
                throw new Exception("This rental is not available for the selected dates");

            var days = (request.EndDate - request.StartDate).Days + 1; // inclusive
            decimal basePrice = days >= 30
                ? (days / 30) * rental.MonthPrice + (days % 30) * rental.OneDayPrice
                : days * rental.OneDayPrice;

            decimal serviceFee = basePrice * 0.15m;
            decimal totalAmount = basePrice + serviceFee;

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var booking = new Booking
                {
                    UserId = request.UserId,
                    RentalId = request.RentalId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    TotalAmount = totalAmount
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                var adminIncome = new AdminIncome
                {
                    BookingId = booking.Id,
                    Amount = serviceFee
                };
                _context.AdminIncomes.Add(adminIncome);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new BookingResponseDto
                {
                    BookingId = booking.Id,
                    Message = "Booking successful! 15% service fee collected.",
                    TotalAmount = totalAmount
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}