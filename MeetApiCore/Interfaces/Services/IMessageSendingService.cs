using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IMessageSendingService
{
    public Task SendMessage(MessageToServer messageToServer, string senderId);
}