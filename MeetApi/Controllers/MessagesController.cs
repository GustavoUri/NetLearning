using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using MeetApi.SignalRContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.Controllers;

[ApiController]
public class MessagesController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly AppContext _db;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(UserManager<User> userManager, AppContext db, IHubContext<ChatHub> hubContext)
    {
        _userManager = userManager;
        _db = db;
        _hubContext = hubContext;
    }

    [HttpPost]
    [Authorize]
    [Route("sendMessage")]
    public async Task<IActionResult> SendMessage(MessageToServer messageToServer)
    {
        var sender = await _userManager.FindByNameAsync(User.Identity?.Name);
        var receiver = await _db.Users.FirstOrDefaultAsync(x => x.Id == messageToServer.ReceiverId);
        if (receiver == null) return BadRequest();

        if (receiver.BlockedUsersId.Contains(sender.Id)) return BadRequest();
        var chat = _db.Chats.FirstOrDefault(x => x.Users.Contains(sender) && x.Users.Contains(receiver));
        var message = new Message()
        {
            Receiver = receiver, Sender = sender, Text = messageToServer.Text,
            SendingTime = messageToServer.SendingTime
        };
        _db.Messages.Add(message);
        if (chat == null)
        {
            chat = new Chat() {Users = {sender, receiver}, Messages = {message}};
            _db.Chats.Add(chat);
        }
        else
        {
            chat.Messages.Add(message);
            _db.Update(chat);
        }

        await _db.SaveChangesAsync();
        var messageToUser = new MessageToUser()
        {
            SenderId = sender.Id,
            SendingTime = messageToServer.SendingTime,
            Text = messageToServer.Text,
            ChatId = chat.Id
        };
        await _hubContext.Clients.User(messageToServer.ReceiverId).SendAsync("Receive", messageToUser);


        return Ok();
    }
}