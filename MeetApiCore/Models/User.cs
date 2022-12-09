using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User : IdentityUser
{
    public int Age { get; set; }
    public string? FullName { get; set; }
    public City? Location { get; set; }
    public string? Gender { get; set; }
    public List<Hobby> Hobbies { get; set; } = new();
    public string? Info { get; set; }
    public List<User> BlockedUsers { get; set; } = new();
    public List<string> Friends { get; set; } = new();
}