using SendEmailAPI.Models;

namespace SendEmailAPI.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(EmailRequest requestParams);
        public Task<bool> SendMultipleEmailAsync(EmailRequest requestParams);
    }
}
