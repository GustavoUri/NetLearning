using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace MeetApi.Models;

public class User : IdentityUser
{
    public int Age { get; set; }
    public string? FullName { get; set; }
    public string? Location { get; set; }
    public string? Gender { get; set; }
    public List<string> Hobbies { get; set; } = new();
    public string? Info { get; set; }
    public string? PhotoPath { get; set; }
    public List<string> BlockedUsersId { get; set; } = new();
    public List<Chat> Chats { get; set; } = new();

    public void UpdateUser(ProfileForm form)
    {
        this.Age = form.Age;
        this.Gender = form.Gender;
        this.Hobbies = form.Hobbies;
        this.Info = form.Info;
        this.Location = form.Location;
        this.FullName = form.FullName;
        // if (form.Photo != null)
        // {
        //     var type = form.Photo?.ContentType.Split("/")[1];
        //     var path = $"Pictures/{this.Id}.{type}";
        //     using (var stream = new FileStream(path, FileMode.Create))
        //     {
        //         form.Photo?.CopyToAsync(stream);
        //     }
        //
        //     this.PhotoPath = path;
        // }
    }
}