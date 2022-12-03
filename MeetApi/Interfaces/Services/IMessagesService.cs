
using MeetApi.Models;

namespace MeetApi.Interfaces.Services;

public interface IMessagesService
{
    Task<List<Message>> GetMessagesAsync(string chatId, string userId);
    Task<List<MessageToClient>> GetMessagesForClientAsync(string chatId, string userId);
}