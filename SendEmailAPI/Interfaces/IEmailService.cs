using SendEmailAPI.Models;

namespace SendEmailAPI.Interfaces
{
    public interface IEmailService
    {
        public Task<string> SendEmailAsync(EmailRequest request);
    }
}
