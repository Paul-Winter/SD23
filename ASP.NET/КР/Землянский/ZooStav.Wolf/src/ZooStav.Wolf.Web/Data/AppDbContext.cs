using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<MediaItem> Media => Set<MediaItem>();
    public DbSet<DiaryEntry> DiaryEntries => Set<DiaryEntry>();
    public DbSet<DiaryComment> DiaryComments => Set<DiaryComment>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Donation> Donations => Set<Donation>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<AppUser>(e =>
        {
            e.HasIndex(x => x.UserName).IsUnique();
        });

        b.Entity<Animal>(e =>
        {
            e.HasIndex(x => x.SubdomainSlug).IsUnique();
            e.HasOne(x => x.Partner).WithMany().HasForeignKey(x => x.PartnerAnimalId).OnDelete(DeleteBehavior.SetNull);
        });

        b.Entity<MediaItem>(e =>
        {
            e.HasOne(x => x.Animal).WithMany(a => a.Media).HasForeignKey(x => x.AnimalId).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(x => new { x.AnimalId, x.Kind });
        });

        b.Entity<DiaryEntry>(e =>
        {
            e.HasOne(x => x.Animal).WithMany(a => a.DiaryEntries).HasForeignKey(x => x.AnimalId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Author).WithMany(u => u.DiaryEntries).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(x => new { x.AnimalId, x.Type, x.OccurredAt });
            e.HasIndex(x => x.AuthorId);
        });

        b.Entity<DiaryComment>(e =>
        {
            e.HasOne(x => x.DiaryEntry).WithMany(d => d.Comments).HasForeignKey(x => x.DiaryEntryId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(x => x.DiaryEntryId);
        });

        b.Entity<AuditLog>(e =>
        {
            e.HasIndex(x => x.TimestampUtc);
            e.HasIndex(x => new { x.EntityName, x.EntityId });
            e.HasIndex(x => x.UserId);
        });

        b.Entity<Donation>(e =>
        {
            e.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            e.HasIndex(x => x.CreatedAt);
        });

        b.Entity<DiaryEntry>().Property(x => x.FoodAmountKg).HasColumnType("decimal(8,2)");
        b.Entity<DiaryEntry>().Property(x => x.WeightKg).HasColumnType("decimal(8,2)");
        b.Entity<DiaryEntry>().Property(x => x.TemperatureC).HasColumnType("decimal(4,1)");
        b.Entity<Animal>().Property(x => x.WeightKg).HasColumnType("decimal(8,2)");
        b.Entity<Animal>().Property(x => x.HeightCm).HasColumnType("decimal(8,2)");
    }
}
