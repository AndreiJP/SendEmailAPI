using SendEmailAPI.Models;
using System.Net.Mail;
using System.Net;
using SendEmailAPI.Interfaces;
using SendEmailAPI.Repositories;

namespace SendEmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogRepository _logRepository;

        public EmailService(IConfiguration configuration, ILogRepository logRepository)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient
            {
                Host = _configuration["EmailAccount:Server"],
                Port = int.Parse(_configuration["EmailAccount:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["EmailAccount:Email"], _configuration["EmailAccount:Password"])
            };
            _logRepository = logRepository;
        }

        public async Task<bool> SendEmailAsync(EmailRequest requestParam)
        {
            try
            {
                if (requestParam.ToEmail.Count == 0)
                    return false;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailAccount:Email"]),
                    Subject = requestParam.Subject,
                    Body = requestParam.Body
                };
                mailMessage.To.Add(requestParam.ToEmail.First());
                await _smtpClient.SendMailAsync(mailMessage);
                DateTime timeStamp = DateTime.Now;
                await AddEmailLogAsync(requestParam, requestParam.ToEmail.First(), timeStamp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> SendMultipleEmailAsync(EmailRequest requestParam)
        {
            try
            {
                if (requestParam.ToEmail.Count == 0)
                    return false;

                foreach (var email in requestParam.ToEmail)
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_configuration["EmailAccount:Email"]),
                        Subject = requestParam.Subject,
                        Body = requestParam.Body,
                    };
                    mailMessage.To.Add(email);
                    await _smtpClient.SendMailAsync(mailMessage);
                    DateTime timeStamp = DateTime.Now;
                    await AddEmailLogAsync(requestParam, email, timeStamp);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task AddEmailLogAsync(EmailRequest requestParam, string toEmail, DateTime sentDate)
        {
            EmailLog log = new EmailLog()
            {
                Recipient = toEmail,
                Subject = requestParam.Subject,
                Body = requestParam.Body,
                SentDate = sentDate
            };

            await _logRepository.AddLogAsync(log);
        }
    }
}
