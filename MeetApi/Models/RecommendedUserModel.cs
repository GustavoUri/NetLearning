
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MeetApi.Models;

public class RecommendedUserModel
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
    public string? Location { get; set; }
    public List<string>? Hobbies { get; set; }
    public string? Info { get; set; }
    public string? Photo { get; set; }

    public RecommendedUserModel(User user)
    {
        Id = user.Id;
        FullName = user.FullName;
        Gender = user.Gender;
        Age = user.Age;
        Location = user.Location;
        Hobbies = user.Hobbies;
        Info = user.Info;
        Photo = user.Photo;

    }
}