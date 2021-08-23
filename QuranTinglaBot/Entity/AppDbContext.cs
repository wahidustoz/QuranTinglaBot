using Microsoft.EntityFrameworkCore;

namespace QuranTinglaBot.Entity
{
    public class AppDbContext : DbContext
    {
        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Surah>().HasIndex(x => x.Number).IsUnique();
        }
    }
}