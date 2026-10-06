using Microsoft.EntityFrameworkCore;

namespace Goat_Zoo.Models
{
    public class GoatZooContext : DbContext
    {
        public GoatZooContext(DbContextOptions<GoatZooContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Login).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}
