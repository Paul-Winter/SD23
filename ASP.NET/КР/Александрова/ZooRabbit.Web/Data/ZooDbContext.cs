using Microsoft.EntityFrameworkCore;
using ZooRabbit.Web.Models;

namespace ZooRabbit.Web.Data;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }

    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<DiaryEntry> DiaryEntries => Set<DiaryEntry>();
}
