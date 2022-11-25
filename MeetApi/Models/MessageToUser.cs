namespace MeetApi.Models;

public class MessageToUser
{
    public string SenderId { get; set; }
    public DateTime SendingTime { get; set; }
    public string Text { get; set; }
    
    public int ChatId { get; set; }
}