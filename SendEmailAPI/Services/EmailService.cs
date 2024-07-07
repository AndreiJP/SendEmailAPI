using SendEmailAPI.Models;
using System.Net.Mail;
using System.Net;
using SendEmailAPI.Interfaces;

namespace SendEmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient
            {
                Host = _configuration["Email:Server"],
                Port = int.Parse(_configuration["Email:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"])
            };
        }

        public async Task<string> SendEmailAsync(EmailRequest request)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Email:Username"]),
                    Subject = request.Subject,
                    Body = $"Nome: {request.Name}\nEmail: {request.Email}\nBody: {request.Body}",
                };

                mailMessage.To.Add(_configuration["Email:Username"]); 
                mailMessage.To.Add(_configuration["Email:PersonalEmail"]);

                await _smtpClient.SendMailAsync(mailMessage);
                return "<spam>Email inviata con successo</spam>";
            }
            catch (Exception)
            {
                return "<spam style='color: red;'>Si è verificato un errore, riprova più tardi</spam>";
            }
        }
    }
}
