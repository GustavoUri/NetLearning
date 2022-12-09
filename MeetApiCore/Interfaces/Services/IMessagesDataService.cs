using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IMessagesDataService
{
    List<Message> GetMessagesBetweenUsers(string firstUserId, string secondUserId);
    List<MessageToClient> GetMessagesBetweenUsersForClient(string askingUserId, string secondUserId);
    public void AddMessage(Message message);
    public Message CreateMessage(MessageToServer messageToServer, string senderId);
    public List<Message> GetAllMessages();
}