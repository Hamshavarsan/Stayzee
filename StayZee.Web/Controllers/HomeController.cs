using Microsoft.AspNetCore.Mvc;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.Interfaces.Iservices;

namespace StayZee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHome([FromBody] HomeRequestDto request)
        {
            var home = await _homeService.CreateHomeAsync(request);
            return Ok(home);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHome(Guid id)
        {
            var home = await _homeService.GetHomeByIdAsync(id);
            if (home == null) return NotFound();
            return Ok(home);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHomes()
        {
            var homes = await _homeService.GetAllHomesAsync();
            return Ok(homes);
        }
    }
}
