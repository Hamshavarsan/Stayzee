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
        private readonly AppDbContext _context;

        public RentalsController(IRentalService service, AppDbContext context)
        {
            _service = service;
            _context = context;
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
        public async Task<IActionResult> Book([FromBody] CreateBookingRequest request)
        {
            var result = await _service.CreateBookingAsync(request);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRentalById(int id)
        {
            var rental = await _service.GetRentalByIdAsync(id);

            if (rental == null)
                return NotFound("Property not found");

            return Ok(rental);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRentals([FromQuery] string? city)
        {
            try
            {
                var rentals = await _service.SearchRentalsAsync(city);
                return Ok(rentals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

    }
}




