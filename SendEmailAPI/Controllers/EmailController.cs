using Microsoft.AspNetCore.Mvc;
using SendEmailAPI.Interfaces;
using SendEmailAPI.Models;

namespace SendEmailAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            return Ok(await _emailService.SendEmailAsync(request));
        }
    }
}
