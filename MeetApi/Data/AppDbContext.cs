using MeetApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Chat> Chats => Set<Chat>();
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

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<User>()
        //         .HasMany(c => c.Chats)
        //         .WithMany(s => s.Users)
        //         .UsingEntity(j => j.ToTable("ChatUser"));
        //
        //     modelBuilder.Entity<Chat>()
        //         .HasMany(c => c.Messages)
        //         .WithOne(s => s.Chat);
        //
        // }
    }
}