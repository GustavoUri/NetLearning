using System.Globalization;
using Entities.Exceptions;
using Entities.Interfaces.Services;
using Entities.Models;
using Entities.SignalRContext;
using Microsoft.AspNetCore.SignalR;
namespace Entities.Services;

public class MessageSendingService : IMessageSendingService
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IUsersDataService _usersDataService;
    private readonly IMessagesDataService _messagesDataService;
    private readonly IChatsDataService _chatsDataService;
    public MessageSendingService(IHubContext<ChatHub> hubContext, IUsersDataService usersDataService, IMessagesDataService messagesDataService,
        IChatsDataService chatsDataService)
    {
        _hubContext = hubContext;
        _usersDataService = usersDataService;
        _messagesDataService = messagesDataService;
        _chatsDataService = chatsDataService;
    }
    public async Task SendMessage(MessageToServer messageToServer, string senderId)
    {
        var sender = _usersDataService.GetUserById(senderId);
        var receiver = _usersDataService.GetUserById(messageToServer.ReceiverId);
        if (receiver.BlockedUsers.Contains(sender))
        {
            throw new BadRequestException("User is blocked by other user");
        }
        var messageForSending = new MessageToClient()
        {
            IsMyMessage = false,
            SenderId = senderId,
            SendingTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            Text = messageToServer.Text
        };
        var message = _messagesDataService.CreateMessage(messageToServer, sender.Id);
        _messagesDataService.AddMessage(message);
        _chatsDataService.AddFriend(sender.Id, messageToServer.ReceiverId);
        //_chatsDataService.AddFriend(messageToServer.ReceiverId, sender.Id);
        await _hubContext.Clients.User(messageToServer.ReceiverId).SendAsync("Receive", messageForSending);
    }
}