using Microsoft.AspNetCore.Identity;

namespace MeetApi.Models;

public class User : IdentityUser
{
    public int Age { get; set; }
    public string? FullName { get; set; }
    public string? Location { get; set; }
    public string? Gender { get; set; }
    public List<string>? Hobbies { get; set; }
    public string? Info { get; set; }

    public string? PhotoName { get; set; }
    // //public string chatsId { get; set; }
    public void UpdateUser(ProfileFormModel form)
    {
        this.Age = form.Age;
        this.Gender = form.Gender;
        this.Hobbies = form.Hobbies;
        this.Info = form.Info;
        this.Location = form.Location;
        this.FullName = form.FullName;
        this.PhotoName = form.PhotoName;
    }
}