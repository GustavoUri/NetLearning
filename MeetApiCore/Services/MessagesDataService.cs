using System.Globalization;
using Entities.Data;
using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Services;

public class MessagesDataService : IMessagesDataService
{
    private readonly IUsersDataService _usersDataService;
    private readonly AppDbContext _db;

    public MessagesDataService(IUsersDataService usersDataService, AppDbContext db)
    {
        _usersDataService = usersDataService;
        _db = db;
    }

    public List<Message> GetMessagesBetweenUsers(string firstUserId, string secondUserId)
    {
        var firstUser = _usersDataService.GetUserById(firstUserId);
        var secondUser = _usersDataService.GetUserById(secondUserId);
        var allMessages = GetAllMessages();
        var result = allMessages.Where(message => (message.Sender == firstUser && message.Receiver == secondUser) ||
                                              (message.Sender == secondUser && message.Receiver == firstUser)).ToList();

        return result;
    }

    public List<MessageToClient> GetMessagesBetweenUsersForClient(string askingUserId, string secondUserId)
    {
        var messages = GetMessagesBetweenUsers(askingUserId, secondUserId);
        var result = new List<MessageToClient>();
        foreach (var message in messages)
        {
            var myMessage = message.Sender.Id == askingUserId;
            var messageToClient = new MessageToClient()
            {
                SenderId = message.Sender.Id,
                SendingTime = message.SendingTime,
                Text = message.Text,
                IsMyMessage = myMessage
            };
            result.Add(messageToClient);
        }

        return result;
    }

    public void AddMessage(Message message)
    {
        _db.Messages.Add(message);
        _db.SaveChanges();
    }

    public Message CreateMessage(MessageToServer messageToServer, string senderId)
    {
        var receiver = _usersDataService.GetUserById(messageToServer.ReceiverId);
        var sender = _usersDataService.GetUserById(senderId);
        var message = new Message()
        {
            Receiver = receiver,
            Sender = sender,
            SendingTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            Text = messageToServer.Text
        };
        return message;
    }

    public List<Message> GetAllMessages()
    {
        var result = _db.Messages
            .Include(message => message.Sender)
            .Include(message => message.Receiver)
            .ToList();
        return result;
    }
    
}