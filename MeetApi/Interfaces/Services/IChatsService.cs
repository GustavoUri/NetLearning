
using MeetApi.Models;
using Chat = MeetApi.Models.Chat;

namespace MeetApi.Interfaces.Services;

public interface IChatsService
{
    List<Chat> GetChats(string userId);
    List<ChatToUser> GetChatsForClient(string userId);

}