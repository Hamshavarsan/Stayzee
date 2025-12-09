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
    }


}
