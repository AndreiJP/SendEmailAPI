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
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest requestParam)
        {
            bool response = await _emailService.SendEmailAsync(requestParam);
            if (response)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("SendMultipleEmail")]
        public async Task<IActionResult> SendMultipleEmail([FromBody] EmailRequest requestParam)
        {
            bool response = await _emailService.SendMultipleEmailAsync(requestParam);
            if (response)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
