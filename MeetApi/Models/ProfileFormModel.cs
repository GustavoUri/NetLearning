﻿namespace MeetApi.Models;

public class ProfileFormModel
{
    public int Age { get; set; }
    public string FullName { get; set; }
    public string Location { get; set; }
    public string Gender { get; set; }
    public List<string> Hobbies { get; set; }
    public string Info { get; set; }
    public string PhotoName { get; set; }
}