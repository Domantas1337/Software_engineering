using Microsoft.EntityFrameworkCore;
using PSIAPI.Models;
namespace PSIAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<LocationItemDto> LocationItems => Set<LocationItemDto>();
        public DbSet<LogItemDto> LogItems => Set<LogItemDto>();
        public DbSet<ReportItemDto> ReportItems => Set<ReportItemDto>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        
    }
}
