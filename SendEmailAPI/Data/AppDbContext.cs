using Microsoft.EntityFrameworkCore;
using SendEmailAPI.Models;

namespace SendEmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definisci DbSet per le tue entità, ad esempio:
        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
