using Microsoft.AspNetCore.SignalR;
using MeetApi;
using MeetApi.Data;
using MeetApi.Models;

namespace MeetApi.SignalRContext;
using System.Security.Claims;
public class CustomUserIdProvider : IUserIdProvider
{
    private AppDbContext db = new AppDbContext();
    
    public virtual string? GetUserId(HubConnectionContext connection)
    {
        var user = db.Users.First(x => x.UserName == connection.User.Identity.Name);
        return user.Id;
    }
}