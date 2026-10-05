using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext(DbContextOptions<ZooContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals => Set<Animal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Animal>()
                .HasIndex(a => a.Slug)
                .IsUnique();
        }
    }
}
