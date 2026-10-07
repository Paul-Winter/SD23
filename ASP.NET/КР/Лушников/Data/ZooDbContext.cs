using Microsoft.EntityFrameworkCore;
using StavZoo.Models;

namespace StavZoo.Data;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }

    public DbSet<Lion> Lions => Set<Lion>();
    public DbSet<LionDiaryEntry> LionDiaryEntries => Set<LionDiaryEntry>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Donation> Donations => Set<Donation>();
    public DbSet<LionPhoto> LionPhotos => Set<LionPhoto>();
}