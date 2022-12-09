using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public new DbSet<User> Users => Set<User>();

        public DbSet<Message> Messages => Set<Message>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Hobby> Hobbies => Set<Hobby>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public AppDbContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123");
        }

        
    }
}