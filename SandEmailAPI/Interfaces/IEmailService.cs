using SandEmailAPI.Models;

namespace SandEmailAPI.Interfaces
{
    public interface IEmailService
    {
        public Task<string> SendEmailAsync(EmailRequest request);
    }
}
