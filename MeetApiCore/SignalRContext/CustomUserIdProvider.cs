using Entities.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Entities.SignalRContext;

[Authorize]
public class CustomUserIdProvider : IUserIdProvider
{
    private readonly IUsersDataService _usersDataService;

    public CustomUserIdProvider(IUsersDataService usersDataService)
    {
        _usersDataService = usersDataService;
    }

    public virtual string? GetUserId(HubConnectionContext connection)
    {
        var user = _usersDataService.GetUserByUserName(connection.User.Identity?.Name);
        return user.Id;
    }
}