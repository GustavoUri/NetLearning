using Microsoft.AspNetCore.SignalR;

namespace MeetApi.SignalRContext;
using System.Security.Claims;
public class CustomUserIdProvider : IUserIdProvider
{
    public virtual string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.Identity?.Name;
    }
}