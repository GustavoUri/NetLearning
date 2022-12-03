

namespace MeetApi.Models;

public class UserToClient 
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
    public string? Location { get; set; }
    public List<string>? Hobbies { get; set; }
    public string? Info { get; set; }

    public UserToClient(User user)
    {
        Id = user.Id;
        FullName = user.FullName;
        Gender = user.Gender;
        Age = user.Age;
        Location = user.Location.name;
        Hobbies = user.Hobbies.Select(hobby => hobby.Name).ToList();
    }
}