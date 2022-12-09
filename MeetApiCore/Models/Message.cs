using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Message
{
    [Key]
    public int Id { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User Receiver { get; set; }
    public string Text { get; set; }
    public string SendingTime { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User Sender { get; set; }

}