using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Entities.SignalRContext
{
    [Authorize]
    public class ChatHub : Hub
    {
    }
}