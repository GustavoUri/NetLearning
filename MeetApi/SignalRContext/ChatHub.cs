using Microsoft.AspNetCore.Authorization; 
using System.Security.Claims;
using MeetApi.Models;
using Microsoft.AspNetCore.SignalR;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.SignalRContext
{
    [Authorize]
    public class ChatHub : Hub
    {
        // AppContext db = new AppContext();
        // [Authorize]
        public async Task Send(Message message)
        {
            // var user = db.Users.First(x => x.UserName == Context.User.Identity.Name);
            // // if (db.Chats.Any(x => x.))
            // // {
            // //     
            // // }
            // await this.Clients.User(message.ReceiverId).SendAsync(message.Text);
            //..........
        }
    }
}