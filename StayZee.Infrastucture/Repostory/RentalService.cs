using Microsoft.EntityFrameworkCore;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
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
                    Message = "Property listed successfully! Role updated to Rentals"
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        // ---------------- GET ALL HOMES -----------------------
        public async Task<List<RentalCardDTO>> GetAllRentals()
        {
            return await _context.Rentals
                .Select(r => new RentalCardDTO
                {
                    Id = r.Id,
                    HomeTitle = r.HomeTitle,
                    HomeLocation = r.HomeLocation,
                    Bedrooms = r.Bedrooms,
                    PetFriendly = r.PetFriendly,
                    OneDayPrice = r.OneDayPrice,
                    MonthPrice = r.MonthPrice,
                    PhotoUrl1 = r.PhotoUrl1
                })
                .ToListAsync();
        }

        // --------------- GET 4 IMAGES OF ONE HOME ---------------
        public async Task<RentalImagesDTO> GetRentalImages(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
                return null;

            return new RentalImagesDTO
            {
                Image1 = rental.PhotoUrl1,
                Image2 = rental.PhotoUrl2,
                Image3 = rental.PhotoUrl3,
                Image4 = rental.PhotoUrl4
            };
        }
    }
}