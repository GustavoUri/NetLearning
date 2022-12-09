using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeetApi.Controllers;
[ApiController]
public class UsersBlockingController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IBlockedUsersDataService _blockedUsersDataService;

    public UsersBlockingController(UserManager<User> userManager, 
        IBlockedUsersDataService blockedUsersDataService)
    {
        _userManager = userManager;
        _blockedUsersDataService = blockedUsersDataService;
    }
    /// <summary>
    /// Blocks other user, so he can't send messages to client,
    /// and client cannot too
    /// </summary>
    /// <remarks>
    ///  
    ///     POST /blockUser
    ///     other user id - "12323dsads123"
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    /// <response code="400">User not found or user is already blocked</response>
    [Authorize]
    [HttpPost]
    [Route("blockUser")]
    public async Task<IActionResult> BlockUser(string idForBlocking)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        _blockedUsersDataService.BlockUser(user.Id, idForBlocking);
        _blockedUsersDataService.BlockUser(idForBlocking, user.Id);
        return Ok();
    }
    /// <summary>
    /// Unblocks other user
    /// </summary>
    /// <remarks>
    ///  
    ///     POST /unblockUser
    ///     other user id - "12323dsads123"
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    /// <response code="400">User not found or user is not blocked</response>
    [Authorize]
    [HttpPost]
    [Route("unblockUser")]
    public async Task<IActionResult> unblockUser(string idForUnblocking)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        _blockedUsersDataService.UnblockUser(user.Id, idForUnblocking);
        return Ok();
    }
    /// <summary>
    /// Returns all user's blocked users
    /// </summary>
    /// <remarks>
    ///  
    ///     GET /blockedUsers
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Authorize]
    [HttpGet]
    [Route("blockedUsers")]
    public async Task<IActionResult> GetBlockedUsers()
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        var result = _blockedUsersDataService.GetBlockedUsersIdOfUser(user.Id);
        return Json(result);
    }
    
    
}