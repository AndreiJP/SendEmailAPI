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
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest requestParams)
        {
            bool response = await _emailService.SendEmailAsync(requestParams);
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
        public async Task<IActionResult> SendMultipleEmail([FromBody] EmailRequest requestParams)
        {
            bool response = await _emailService.SendMultipleEmailAsync(requestParams);
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
