using SendEmailAPI.Models;

namespace SendEmailAPI.Repositories
{
    public interface ILogRepository
    {
        public Task AddLogAsync(EmailLog log);
    }
}
