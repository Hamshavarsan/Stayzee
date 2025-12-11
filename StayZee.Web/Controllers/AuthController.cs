using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayZee.Application.DTOs.RequestDTO;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Infrastructure.Data;

namespace StayZee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;

        public AuthController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
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

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                // Verify logic typically handled by frontend for APIs, but using redirect per request
                return BadRequest("Invalid verification code.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationCode == code);
            if (user == null)
            {
                return BadRequest("Verification failed. Code not found.");
            }

            if (user.VerificationExpiresAt.HasValue && user.VerificationExpiresAt.Value < DateTime.UtcNow)
            {
                return BadRequest("Verification code expired. Please register again.");
            }

            // Verify the user
            user.IsVerified = true;
            user.VerificationCode = null;
            user.VerificationExpiresAt = null;
            await _context.SaveChangesAsync();

            // Return JSON for frontend to handle navigation
            return Ok(new { message = "Account verified successfully!" });
        }
    }
}
