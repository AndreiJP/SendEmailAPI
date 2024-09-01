using SendEmailAPI.Models;

namespace SendEmailAPI.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(EmailRequest requestParam);
        public Task<bool> SendMultipleEmailAsync(EmailRequest requestParam);
    }
}
