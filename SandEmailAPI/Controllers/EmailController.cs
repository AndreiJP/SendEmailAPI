using Microsoft.AspNetCore.Mvc;
using SandEmailAPI.Interfaces;
using SandEmailAPI.Models;

namespace SandEmailAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        // Una dipendenza su un servizio email che verrà iniettata
        private readonly IEmailService _emailService;

        // Costruttore della classe che accetta un servizio email come parametro
        // Questo servizio verrà iniettato automaticamente dal framework di ASP.NET Core
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // Metodo per gestire le richieste POST a "www.miosito.com/email"
        // Questo metodo accetta un oggetto EmailRequest come parametro, che viene deserializzato dal corpo della richiesta
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            // Chiama il metodo SendEmailAsync del servizio email con l'oggetto EmailRequest come parametro
            // Se l'invio dell'email va a buon fine, restituisce una risposta HTTP 200 (OK)
            // Se qualcosa va storto, restituisce un errore HTTP 500 (Internal Server Error)
            return Ok(await _emailService.SendEmailAsync(request));
        }
    }
}
