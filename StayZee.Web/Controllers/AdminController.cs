using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayZee.Application.Interfaces.Iservices;
using System;
using System.Threading.Tasks;

namespace StayZee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [Authorize(Roles = "Admin")] // Uncomment when Authentication is fully set up and roles are defined
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("users/{id}/block")]
        public async Task<IActionResult> BlockUser(int id)
        {
            await _adminService.BlockUserAsync(id);
            return Ok(new { message = "User blocked successfully." });
        }

        [HttpPost("users/{id}/unblock")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            await _adminService.UnblockUserAsync(id);
            return Ok(new { message = "User unblocked successfully." });
        }

        [HttpGet("homes/pending")]
        public async Task<IActionResult> GetPendingHomes()
        {
            var homes = await _adminService.GetPendingHomesAsync();
            return Ok(homes);
        }

        [HttpPost("homes/{id}/approve")]
        public async Task<IActionResult> ApproveHome(Guid id)
        {
            await _adminService.ApproveHomeAsync(id);
            return Ok(new { message = "Home approved successfully." });
        }

        [HttpPost("homes/{id}/reject")]
        public async Task<IActionResult> RejectHome(Guid id)
        {
            await _adminService.RejectHomeAsync(id);
            return Ok(new { message = "Home rejected successfully." });
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _adminService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("rentals")]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await _adminService.GetAllRentalsAsync();
            return Ok(rentals);
        }
    }
}
