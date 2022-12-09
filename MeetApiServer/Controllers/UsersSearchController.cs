using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MeetApi.Controllers;

[ApiController]
public class UsersSearchController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUserSearchService _searchService;

    public UsersSearchController(UserManager<User> userManager, IUserSearchService userSearchService)
    {
        _userManager = userManager;
        _searchService = userSearchService;
    }
    /// <summary>
    /// Returns recommended users
    /// </summary>
    /// <remarks>
    ///  
    ///     GET /recommendedUser
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Route("recommendedUsers")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> RecommendedUsers()
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name);
        var result = _searchService.GetUsersIdByRecommendation(user.Id);
        return Json(result);
    }
    /// <summary>
    /// Returns filtered users
    /// </summary>
    /// <remarks>
    ///  If you don't want to fo filtration by locations, gender or hobbies,
    ///  just don't add them
    ///  But age borders are necessary
    ///     POST /filteredUsers
    ///     {
    ///         "firstAgeBorder": 0,
    ///         "secondAgeBorder": 0,
    ///         "locations": [
    ///         "string"
    ///         ],
    ///         "gender": "string",
    ///         "hobbies": [
    ///         "string"
    ///         ]
    ///     }
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Route("filteredUsers")]
    [HttpPost]
    public async Task<IActionResult> GetUsersByFilter(Filter filter)
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name);
        var result = _searchService.GetUsersIdByFilter(filter, user.Id);
        return Json(result);
    }
}