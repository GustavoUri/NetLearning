

namespace MeetApi.Models;

public class ChatToUser 
{
    public string? PhotoPath { get; set; }
    public string? Name { get; set; }
    public int Id { get; set; }
    public string OtherUserId { get; set; }
    public bool IsBlocked { get; set; }
}