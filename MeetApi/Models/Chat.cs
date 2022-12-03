using System.ComponentModel.DataAnnotations;

namespace MeetApi.Models;

public class Chat 
{
    [Key] public int Id { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] 
    public List<User> Users { get; set; } = new();

    public List<Message> Messages { get; set; } = new();
}