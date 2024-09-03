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

        public async Task<bool> SendMultipleEmailAsync(EmailRequest requestParams)
        {
            if (requestParams == null || requestParams.ToEmails == null || !requestParams.ToEmails.Any())
                return false;

            foreach (var toEmail in requestParams.ToEmails)
            {
                var singleEmailRequest = new EmailRequest
                {
                    Subject = requestParams.Subject,
                    Body = requestParams.Body,
                    ToEmails = new List<string> { toEmail }
                };

                var result = await SendEmailAsync(singleEmailRequest);
                if (!result)
                    return false;
            }

            return true;
        }

        public async Task<bool> SendEmailAsync(EmailRequest requestParams)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailAccount:Email"]),
                    Subject = requestParams.Subject,
                    Body = requestParams.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(requestParams.ToEmails.First());

                await _smtpClient.SendMailAsync(mailMessage);

                await AddEmailLogAsync(requestParams, requestParams.ToEmails.First(), EmailStatus.Success);

                return true;
            }
            catch (Exception ex)
            {
                await AddEmailLogAsync(requestParams, requestParams.ToEmails.First(), EmailStatus.Error, ex.Message);
                return false;
            }
        }





        private async Task AddEmailLogAsync(EmailRequest requestParam, string toEmail, EmailStatus status, string errorMessage = "")
        {
            EmailLog log = new EmailLog()
            {
                Recipient = toEmail,
                Subject = requestParam.Subject,
                Body = requestParam.Body,
                SentDate = DateTime.Now,
                Status = status,
                ErrorMessage = errorMessage
            };

            await _logRepository.AddLogAsync(log);
        }
    }
}
