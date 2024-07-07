using SendEmailAPI.Models;
using System.Net.Mail;
using System.Net;
using SendEmailAPI.Interfaces;

namespace SendEmailAPI.Services
{
    public class EmailService : IEmailService
    {
        // _smtpClient sarà usato per inviare le email
        private readonly SmtpClient _smtpClient;
        // _configuration sarà usato per ottenere le configurazioni dell'applicazione
        private readonly IConfiguration _configuration;

        // Il costruttore inizializza _smtpClient con le configurazioni ottenute da _configuration
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient
            {
                // Otteniamo l'host e la porta del server SMTP da _configuration
                Host = _configuration["Email:Server"],
                Port = int.Parse(_configuration["Email:Port"]),
                EnableSsl = true,
                // Impostiamo le credenziali di autenticazione con il server SMTP
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"])
            };
        }

        // Questo metodo prende un oggetto EmailRequest, crea un oggetto MailMessage da esso, e invia l'email
        public async Task<string> SendEmailAsync(EmailRequest request)
        {
            try
            {
                // Creiamo l'oggetto MailMessage
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Email:Username"]),
                    Subject = request.Subject,
                    Body = $"Nome: {request.Name}\nEmail: {request.Email}\nBody: {request.Body}",
                };

                mailMessage.To.Add(_configuration["Email:Username"]); 
                mailMessage.To.Add(_configuration["Email:PersonalEmail"]);

                // Inviamo l'email
                await _smtpClient.SendMailAsync(mailMessage);
                // Se l'email è stata inviata con successo, restituiamo true
                return "<spam>Email inviata con successo</spam>";
            }
            catch (Exception)
            {
                // Se c'è un errore nell'invio dell'email, restituiamo false
                return "<spam style='color: red;'>Si è verificato un errore, riprova più tardi</spam>";
            }
        }
    }
}
