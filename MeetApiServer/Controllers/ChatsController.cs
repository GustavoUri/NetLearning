using System.Diagnostics;
using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MeetApi.Controllers;

[ApiController]
public class ChatsController : Controller
{
    private readonly IChatsDataService _chatsDataService;
    private readonly UserManager<User> _userManager;

    public ChatsController(UserManager<User> userManager, IChatsDataService chatsDataService)
    {
        _userManager = userManager;
        _chatsDataService = chatsDataService;
    }
    /// <summary>
    /// Returns all user ids with which there are messages
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     GET /getChats
    ///     
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Authorize]
    [HttpGet]
    [Route("getChats")]
    public async Task<IActionResult> GetChats()
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        var result = _chatsDataService.GetAllFriendsIdsOfUser(user.Id);
        return Json(result);
    }
}