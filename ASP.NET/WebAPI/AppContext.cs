using Microsoft.EntityFrameworkCore;

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
                new User() { Id = Guid.NewGuid().ToString(), Login = "111FirstUser111", Password = "11111" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "XyJIu*Gun", Password = "123456" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "DaRK-RaiN", Password = "qwerty" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "Black_Dragon", Password = "password" },
                new User() { Id = Guid.NewGuid().ToString(), Login = "___Max_Frei___", Password = "sirmax" }
            );
        }
    }
}
