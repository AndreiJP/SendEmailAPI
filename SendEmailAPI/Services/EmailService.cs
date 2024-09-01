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
                Host = _configuration["EmailAccount:Server"],
                Port = int.Parse(_configuration["EmailAccount:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["EmailAccount:Email"], _configuration["EmailAccount:Password"])
            };
        }

        public async Task<bool> SendEmailAsync(EmailRequest requestParam)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailAccount:Email"]),
                    Subject = requestParam.Subject,
                    Body = requestParam.Body,
                };

                mailMessage.To.Add(requestParam.ToEmail); 

                await _smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
