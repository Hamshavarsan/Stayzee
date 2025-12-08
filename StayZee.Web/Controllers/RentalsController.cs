// RentalsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Infrastructure.Data;

namespace StayZee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _service;
        private readonly AppDbContext _context;   // <-- DbContext inject pannu

        public RentalsController(IRentalService service, AppDbContext context)
        {
            _service = service;
            _context = context;   // <-- idhu must
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRental([FromForm] CreateRentalRequest request)
        {
            var result = await _service.CreateRental(request);

            // Customer → "Rentals" role ku change pannu
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user != null && user.Role == "Customer")
            {
                user.Role = "Rentals";                    // ← Nee solra value
                await _context.SaveChangesAsync();
            }

            return Ok(new RentalResponse
            {
                RentalId = result.RentalId,
                Message = "Rental created successfully! Welcome to Rentals Club!"
            });
        }
    }
      
}
