using Microsoft.EntityFrameworkCore;

namespace WebAppMVC.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
