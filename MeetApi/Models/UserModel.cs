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
    public string? Photo { get; set; }
    // //public string chatsId { get; set; }
    public void UpdateUser(ProfileFormModel formModel)
    {
        this.Age = formModel.Age;
        this.Gender = formModel.Gender;
        this.Hobbies = formModel.Hobbies;
        this.Info = formModel.Info;
        this.Location = formModel.Location;
        this.FullName = formModel.FullName;
        this.Photo = formModel.Photo;
    }
}