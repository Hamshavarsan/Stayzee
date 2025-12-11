using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore.Metadata.Internal; // Removed to resolve ambiguity
using StayZee.Application.Interfaces.Iservices;
using StayZee.Domain.Entities;

namespace StayZee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertySerivice _service;

        public PropertiesController(IPropertySerivice service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? search, string? city)
        {
            var list = await _service.GetPropertiesAsync(search, city, null, null);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var prop = await _service.GetByIdAsync(id);
            if (prop == null) return NotFound();
            return Ok(prop);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StayZee.Domain.Entities.Property p)
        {
            await _service.CreateAsync(p); // Removed assignment to a variable since the method returns void
            return Ok("Property created successfully."); // Added a success message
        }
    }
}
