using Microsoft.EntityFrameworkCore;

namespace Goat_Zoo.Models
{
    public class GoatZooContext : DbContext
    {
        public GoatZooContext(DbContextOptions<GoatZooContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
