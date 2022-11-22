using Microsoft.AspNetCore.SignalR;
using MeetApi;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.SignalRContext;
using System.Security.Claims;
public class CustomUserIdProvider : IUserIdProvider
{
    private AppContext db = new AppContext();
    
    public virtual string? GetUserId(HubConnectionContext connection)
    {
        var user = db.Users.First(x => x.UserName == connection.User.Identity.Name);
        return user.Id;
    }
}