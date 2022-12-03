// using MeetApi.Data;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Identity;
// using MeetApi.Models;
// using Microsoft.AspNetCore.Authorization;
//
// namespace MeetApi.Controllers;
//
// [ApiController]
// public class UsersSearchController : Controller
// {
//     private readonly UserManager<User> _userManager;
//     private readonly AppDbContext _db;
//
//     public UsersSearchController(UserManager<User> userManager, AppDbContext db)
//     {
//         _userManager = userManager;
//         _db = db;
//     }
//
//     [Route("getRecommendedUsers")]
//     [HttpGet]
//     [Authorize]
//     public async Task<IActionResult> RecommendedUsers()
//     {
//         var user = await _userManager.FindByNameAsync(User.Identity!.Name);
//         var result = await GetUsers(user);
//         return Json(result);
//     }
//
//     private Task<List<UserToClient>> GetUsers(User mainUser)
//     {
//         var coincidenceToUser = new Dictionary<UserToClient, int>();
//         foreach (var user in _db.Users)
//         {
//             var count = 0;
//             if (user.Location == mainUser.Location)
//                 count++;
//             if (user.Gender == mainUser.Gender)
//                 count++;
//             if (mainUser.Hobbies != null && user.Hobbies != null)
//                 count += mainUser.Hobbies.Count(user.Hobbies.Contains);
//             coincidenceToUser.Add(new UserToClient(user), count);
//         }
//
//         var result = coincidenceToUser.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
//         return Task.FromResult(result);
//     }
//
//     [Route("getFilteredUsers")]
//     [HttpPost]
//     public Task<IActionResult> GetUsersByFilter(Filter filter)
//     {
//         var result = _db.Users.ToList();
//         if (filter.Gender != null)
//         {
//             result =  result.Where(x => x.Gender == filter.Gender).ToList();
//         }
//
//         result = result.Where(x => x.Age >= filter.FirstAgeBorder && x.Age <= filter.SecondAgeBorder).ToList();
//
//         if (filter.Hobbies != null)
//         {
//             result = result.Where(x =>
//                     x.Hobbies != null && x.Hobbies.OrderBy(b => b).SequenceEqual(filter.Hobbies.OrderBy(y => y)))
//                 .ToList();
//         }
//
//         if (filter.Locations != null)
//         {
//             result = result.Where(user => user.Location != null && filter.Locations.Contains(user.Location)).ToList();
//         }
//
//         var users = new List<UserToClient>();
//
//         foreach (var user in result)
//         {
//             users.Add(new UserToClient(user));
//         }
//
//         return Task.FromResult<IActionResult>(Json(users));
//     }
// }