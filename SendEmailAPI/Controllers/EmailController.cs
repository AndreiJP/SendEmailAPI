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
        // Questo metodo accetta un oggetto EmailRequest come parametro, che viene deserializzato dal corpo della richiesta
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            // Chiama il metodo SendEmailAsync del servizio email con l'oggetto EmailRequest come parametro
            return Ok(await _emailService.SendEmailAsync(request));
        }
    }
}
