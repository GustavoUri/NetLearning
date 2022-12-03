// using MeetApi.Data;
// using MeetApi.Interfaces.Services;
// using MeetApi.Models;
// using Microsoft.EntityFrameworkCore;
//
// namespace MeetApi.Services;
//
// public class ChatsService : IChatsService
// {
//     private readonly AppDbContext _db;
//
//     public ChatsService( AppDbContext db)
//     {
//         _db = db;
//     }
//     public List<Chat> GetChats(string userId)
//     {
//         var user = _db.Users.First(x => x.Id == userId);
//         var chats = _db.Chats.Include(x => x.Users)
//             .Include(x => x.Messages).Where(x => x.Users.Contains(user)).ToList();
//
//         return chats;
//     }
//
//     public List<ChatToUser> GetChatsForClient(string userId)
//     {
//         var chats = GetChats(userId);
//         var result = new List<ChatToUser>();
//         foreach (var chat in chats)
//         {
//             var otherUser = chat.Users.First(x => x.Id != user.Id);
//             var chatName = otherUser.FullName;
//             var chatPhoto = otherUser.PhotoPath;
//             bool isBlocked = chat.Users.First(x => x.Id != user.Id).BlockedUsers.Contains(user);
//             result.Add(new ChatToUser()
//             {
//                 Id = chat.Id, Name = chatName, PhotoPath = chatPhoto,
//                 OtherUserId = chat.Users.First(x => x.Id != user.Id).Id,
//                 IsBlocked = isBlocked
//             });
//         }
//
//         return result;
//     }
// }