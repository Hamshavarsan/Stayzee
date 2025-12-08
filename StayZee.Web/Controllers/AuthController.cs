using Microsoft.AspNetCore.Mvc;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.Interfaces.Iservices;

namespace StayZee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return BadRequest(new { message = "Invalid username or password!" });

            return Ok(result);
        }
    }
}
