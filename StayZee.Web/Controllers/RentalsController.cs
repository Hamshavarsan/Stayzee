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
        public async Task<IActionResult> Book([FromBody] CreateBookingRequest request)
        {
            var result = await _service.CreateBookingAsync(request);
            return Ok(result);
        }

    }




}
