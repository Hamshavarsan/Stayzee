// RentalsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.DTOs.ResponseDTO;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Infrastructure.Data;
using StayZee.Infrastructure.Repository;

namespace StayZee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _service;

        public RentalsController(IRentalService service)
        {
            _service = service;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateRental([FromForm] CreateRentalRequest request)
        {
            var result = await _service.CreateRental(request);
            return Ok(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRentals()
        {
            try
            {
                var rentals = await _service.GetAllRentalsAsync();
                return Ok(rentals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        [HttpPost("book")]
        [Authorize]  // Requires authentication (use JWT or your auth mechanism)
        public async Task<IActionResult> BookRental([FromBody] BookRequest request)
        {
            // UserId can be fetched from claims if using auth: int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _service.CreateBookingAsync(request.RentalId, request.UserId);
            return Ok(response);
        }

    }




}
