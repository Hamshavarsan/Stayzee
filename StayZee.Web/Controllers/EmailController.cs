using Microsoft.AspNetCore.Mvc;
using StayZee.Application.Interfaces.Iservices;
using System.Threading.Tasks;

namespace StayZee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            await _emailService.SendEmailAsync(
                request.To,
                request.Subject,
                request.Body
            );

            return Ok("Email Sent!");
        }
    }

    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
