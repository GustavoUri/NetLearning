using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MeetApi.Models
{
    public class AppContext : IdentityDbContext<User>
    {
        //public DbSet<User> Users => Set<User>();

        //public DbSet<Message> Messages => Set<Message>();
        //public DbSet<string> Names => Set<string>();
        public AppContext(DbContextOptions<AppContext> options): base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        
        public AppContext()
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