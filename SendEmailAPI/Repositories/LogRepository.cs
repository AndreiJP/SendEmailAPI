using Microsoft.EntityFrameworkCore;
using SendEmailAPI.Data;
using SendEmailAPI.Models;

namespace SendEmailAPI.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly AppDbContext  _context;

        public LogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(EmailLog entity)
        {
            await _context.EmailLogs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
