// using MeetApi.Interfaces.Services;
// using MeetApi.Models;
//
// namespace MeetApi.Services;
//
// public class MessagesService : IMessagesService
// {
//     private readonly IChatsService _chatsService;
//
//     private MessagesService(IChatsService chatsService)
//     {
//         _chatsService = chatsService;
//     }
//     public Task<List<Message>> GetMessagesAsync(string chatId, string userId)
//     {
//         var chat = _chatsService.GetChats()
//     }
//
//     public Task<List<MessageToClient>> GetMessagesForClientAsync(string id, User user)
//     {
//         throw new NotImplementedException();
//     }
// }