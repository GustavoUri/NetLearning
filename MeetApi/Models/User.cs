﻿
using Microsoft.AspNetCore.Identity;

namespace MeetApi.Models;

public class User : IdentityUser
{
    public int Age { get; set; }
    public string? FullName { get; set; }
    public City? Location { get; set; }
    public string? Gender { get; set; }
    public List<Hobby> Hobbies { get; set; } = new();
    public string? Info { get; set; }
    public string? PhotoPath { get; set; }
    public List<User> BlockedUsers { get; set; } = new();
    public List<Chat> Chats { get; set; } = new();

    public User()
    {
        PhotoPath = "Pictures/classicPhoto/classicPhoto";
    }
    // public void UpdateUser(ProfileForm form)
    // {
    //     this.Age = form.Age;
    //     this.Gender = form.Gender;
    //     this.Hobbies = form.Hobbies;
    //     this.Info = form.Info;
    //     this.Location = form.Location;
    //     this.FullName = form.FullName;
    // }
}