

namespace Entities.Models;

public class MessageToClient 
{
    public string SenderId { get; set; }
    public string SendingTime { get; set; }
    public string Text { get; set; }
    public bool IsMyMessage { get; set; }
}