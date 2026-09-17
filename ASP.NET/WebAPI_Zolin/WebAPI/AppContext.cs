using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace WebAPI
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = Guid.NewGuid().ToString(), Login = "qweqwe123123", Password = "11111" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "TheFirst", Password = "qweqwe" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "Obaba", Password = "zxc67" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "MrVarenik", Password = "popipopipopipu" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "apelsin", Password = "banan" });
        }
    }
}
