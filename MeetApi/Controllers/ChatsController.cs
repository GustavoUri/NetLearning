// using MeetApi.Data;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Identity;
// using MeetApi.Models;
// using MeetApi.SignalRContext;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.SignalR;
// using Microsoft.EntityFrameworkCore;
//
// namespace MeetApi.Controllers;
//
// [ApiController]
// public class ChatsController : Controller
// {
//     private readonly UserManager<User> _userManager;
//     private readonly AppDbContext _db;
//
//     public ChatsController(UserManager<User> userManager, AppDbContext db)
//     {
//         _userManager = userManager;
//         _db = db;
//     }
//
//     [Authorize]
//     [HttpGet]
//     [Route("GetChats")]
//     public async Task<IActionResult> GetChats()
//     {
//         var result = new List<ChatToUser>();
//         var user = _db.Users.First(x => x.UserName == User.Identity.Name);
//         var chats = GetUserRawChats(user);
//
//         foreach (var chat in chats)
//         {
//             var otherUser = chat.Users.First(x => x.Id != user.Id);
//             var chatName = otherUser.FullName;
//             var chatPhoto = otherUser.PhotoPath;
//             bool isBlocked = chat.Users.First(x => x.Id != user.Id).BlockedUsersId.Contains(user.Id);
//             result.Add(new ChatToUser()
//             {
//                 Id = chat.Id, Name = chatName, PhotoPath = chatPhoto,
//                 OtherUserId = chat.Users.First(x => x.Id != user.Id).Id,
//                 IsBlocked = isBlocked
//             });
//         }
//
//
//         return Json(result);
//     }
//
//     [NonAction]
//     public List<Chat> GetUserRawChats(User user)
//     {
//         var chats = _db.Chats.Include(x => x.Users)
//             .Include(x => x.Messages).Where(x => x.Users.Contains(user)).ToList();
//
//         return chats;
//     }
//
//     [Authorize]
//     [HttpGet]
//     [Route("GetMessages")]
//     public async Task<IActionResult> GetChat(int chatId)
//     {
//         var user = await _userManager.FindByNameAsync(User.Identity.Name);
//         var chats = GetUserRawChats(user);
//         var result = new List<MessageToClient>();
//         var chat = chats.FirstOrDefault(x => x.Id == chatId);
//         if (chat != null)
//         {
//             foreach (var message in chat.Messages)
//             {
//                 var mesToUser = new MessageToClient()
//                 {
//                     SenderId = message.Sender.Id,
//                     SendingTime = message.SendingTime,
//                     Text = message.Text,
//                     ChatId = chat.Id
//                 };
//                 result.Add(mesToUser);
//             }
//         }
//
//         return Json(result);
//     }
//
//     [Authorize]
//     [HttpPost]
//     [Route("BlockUser")]
//     public async Task<IActionResult> BlockUser(string userId)
//     {
//         var user = await _userManager.FindByNameAsync(User.Identity.Name);
//         var blockedUser = _db.Users.FirstOrDefault(x => x.Id == userId);
//         if (blockedUser != null)
//             user.BlockedUsersId.Add(userId);
//         await _userManager.UpdateAsync(user);
//         return Ok();
//     }
//
//     [Authorize]
//     [HttpPost]
//     [Route("UnBlockUser")]
//     public async Task<IActionResult> UnblockUser(string userId)
//     {
//         var user = await _userManager.FindByNameAsync(User.Identity.Name);
//         if (user.BlockedUsersId.Contains(userId))
//         {
//             user.BlockedUsersId.Remove(userId);
//         }
//
//         await _userManager.UpdateAsync(user);
//         return Ok();
//     }
// }