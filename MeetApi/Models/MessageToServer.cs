namespace MeetApi.Models;

public class MessageToServer
{
    public DateTime SendingTime { get; set; }
    public string Text { get; set; }
    public string ReceiverId { get; set; }
}