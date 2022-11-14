using Microsoft.AspNetCore.Authorization; // для атрибута Authorize
using System.Security.Claims;   // для ClaimTypes
using Microsoft.AspNetCore.SignalR;
 
namespace MeetApi.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        [Authorize]
        public Task Send(string message, string to)
        {
            var user = Context.User;
            var userName = user?.Identity?.Name;
            // получаем роль
            var userRole = user?.FindFirst(ClaimTypes.Role)?.Value;
            // принадлежит ли пользователь роли "admins"
            var isAdmin = user?.IsInRole("admin");
            return Task.CompletedTask;
            //..........
        }
    }
}