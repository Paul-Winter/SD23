using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Data
{
    public class ZooContext : IdentityDbContext<ApplicationUser>
    {
        public ZooContext(DbContextOptions<ZooContext> options) : base(options){}

        public DbSet<Animal> Animals => Set<Animal>();
        public DbSet<DiaryEntry> DiaryEntries => Set<DiaryEntry>();
        public DbSet<Donation> Donations => Set<Donation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Animal>()
                .HasIndex(a => a.Slug)
                .IsUnique();

            modelBuilder.Entity<DiaryEntry>()
                .HasOne(d => d.Animal)
                .WithMany(a => a.DiaryEntries)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiaryEntry>()
                .HasOne(d => d.RecordedBy)
                .WithMany()
                .HasForeignKey(d => d.RecordedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AnimalPhoto>()
                .HasOne(p => p.Animal)
                .WithMany(a => a.Photos)
                .HasForeignKey(p => p.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);


        }

        public DbSet<AnimalPhoto> AnimalPhotos => Set<AnimalPhoto>();
    }
}