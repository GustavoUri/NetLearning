

namespace MeetApi.Models;

public class MessageToClient 
{
    public string SenderId { get; set; }
    public string SendingTime { get; set; }
    public string Text { get; set; }
    
    public int ChatId { get; set; }
}