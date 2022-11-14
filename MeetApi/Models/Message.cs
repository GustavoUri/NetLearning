using System.ComponentModel.DataAnnotations;

namespace MeetApi.Models;

public class Message
{
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]                                                                  
    public string Text { get; set; }
    public DateTime mesDate { get; set; }
    public string UserId { get; set; }
    public User Sender { get; set; }
}