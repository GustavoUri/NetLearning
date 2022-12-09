namespace Entities.Models;

public class Profile
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
    public string? Location { get; set; }
    public List<string>? Hobbies { get; set; }
    public string? Info { get; set; }
}