

namespace MeetApi.Models;

public class MessageToServer 
{
    public string SendingTime { get; set; }
    public string Text { get; set; }
    public string ReceiverId { get; set; }
}