using System.ComponentModel.DataAnnotations;

namespace MeetApi.Models;

public class Message
{
    [System.Text.Json.Serialization.JsonIgnore]
    public Chat? Chat { get; set; }
    [Key]
    public int Id { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User Receiver { get; set; }
    public string Text { get; set; }
    public DateTime SendingTime { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User Sender { get; set; }
    public int? ChatId { get; set; }

}