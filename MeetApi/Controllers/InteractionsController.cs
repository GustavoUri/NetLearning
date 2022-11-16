using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InteractionsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly AppContext _db; 

    public InteractionsController(UserManager<User> userManager, AppContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    [Route("RecommendedUsers")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> RecommendedUsers()
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name);
        var result = await GetUsers(user);
        return Json(result);
    }

    private Task<List<RecommendedUserModel>> GetUsers(User mainUser)
    {
        var coincidenceToUser = new Dictionary<RecommendedUserModel, int>();
        foreach (var user in _db.Users)
        {
            var count = 0;
            if (user.Location == mainUser.Location)
                count++;
            if (user.Gender == mainUser.Gender)
                count++;
            if (mainUser.Hobbies != null && user.Hobbies != null)
                count += mainUser.Hobbies.Count(user.Hobbies.Contains);
            coincidenceToUser.Add(new RecommendedUserModel(user), count);
        }

        var result = coincidenceToUser.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
        return Task.FromResult(result);
    }
}