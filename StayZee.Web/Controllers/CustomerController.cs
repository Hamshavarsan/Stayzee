using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayZee.Domain.Entities;
using StayZee.Infrastructure.Data;

namespace StayZee.Web.Controllers
{

    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly AppDbContext _db;
        public CustomerController(AppDbContext db) => _db = db;

        [HttpPost("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] favorite fav)
        {
            // basic check: prevent duplicates
            var exists = await _db.Favorites.AnyAsync(f => f.CustomerId == fav.CustomerId && f.PropertyId == fav.PropertyId);
            if (exists) return BadRequest("Already favorited");
            await _db.Favorites.AddAsync(fav);
            await _db.SaveChangesAsync();
            return Ok(fav);
        }

        [HttpGet("favorites/{customerId}")]
        public async Task<IActionResult> GetFavorites(Guid customerId)
        {
            var favs = await _db.Favorites.Where(f => f.CustomerId == customerId)
                        .Join(_db.Properties, f => f.PropertyId, p => p.Id, (f, p) => p)
                        .ToListAsync();
            return Ok(favs);
        }

        [HttpPost("booking")]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            //booking.Status = "Pending";
            await _db.Bookings.AddAsync(booking);
            await _db.SaveChangesAsync();
            return Ok(booking);
        }

        [HttpGet("bookings/{customerId}")]
       // public async Task<IActionResult> GetBookings(Guid customerId)
       // {
       ////     var bs = await _db.Bookings.Where(b => b.CustomerId == customerId).ToListAsync();
       //     return Ok(bs);
       // }

        // Profile endpoints (simplified)
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            var u = await _db.Users.FindAsync(id);
            if (u == null) return NotFound();
            u.PasswordHash = "";
            return Ok(u);
        }

        [HttpPost("profile/update")]
        public async Task<IActionResult> UpdateProfile([FromBody] User user)
        {
            var dbu = await _db.Users.FindAsync(user.Id);
            if (dbu == null) return NotFound();
            dbu.Name = user.Name;
            dbu.PhoneNumber = user.PhoneNumber;
            // ignore password changes for now
            await _db.SaveChangesAsync();
            return Ok(dbu);
        }
    }
}
