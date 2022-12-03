using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetApi.Models;

public class Message
{
    [System.Text.Json.Serialization.JsonIgnore]
    [NotMapped]
    public Chat? Chat { get; set; }
    [Key]
    public int Id { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    [NotMapped]
    public User Receiver { get; set; }
    public string Text { get; set; }
    public DateTime SendingTime { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    [NotMapped]
    public User Sender { get; set; }

}