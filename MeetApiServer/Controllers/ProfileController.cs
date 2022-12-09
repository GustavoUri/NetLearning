using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MeetApi.Controllers;

[ApiController]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IProfileService _profileService;
    private readonly IPicturesDataService _picturesDataService;

    public ProfileController(UserManager<User> userManager,
        IProfileService profileService, IPicturesDataService picturesDataService)
    {
        _userManager = userManager;
        _profileService = profileService;
        _picturesDataService = picturesDataService;
    }
    /// <summary>
    /// Updates user's profile
    /// </summary>
    /// <remarks>
    /// For location use locations from /cities
    /// For hobbies use hobbies from /hobbies
    /// For gender use only "M" - male, or "F" - female
    ///  
    ///     POST /updateProfile
    ///     {
    ///         "age": 19,
    ///         "fullName": "Garipov Nazar Whovich",
    ///         "location": "Yerevan",
    ///         "gender": "M",
    ///         "hobbies": [
    ///         "gaming", "coding"
    ///         ],
    ///         "info": "Hi, i am just dude"
    ///     }
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Authorize]
    [Route("updateProfile")]
    [HttpPut]
    public async Task<IActionResult> AddProfileForm(ProfileFormToServer formToServer)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        _profileService.UpdateProfile(user.Id, formToServer);
        return Ok();
    }
    /// <summary>
    /// Updates user's profile picture
    /// </summary>
    /// <remarks>
    /// 
    ///  
    ///     POST /updateProfilePicture
    ///     
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Authorize]
    [Route("updateProfilePicture")]
    [HttpPut]
    public async Task<IActionResult> UpdateProfilePicture(IFormFile picture)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        await _picturesDataService.UpdateUserProfilePictureAsync(picture, user.Id);
        return Ok();
    }
    /// <summary>
    /// Returns user's profile
    /// </summary>
    /// <remarks>
    ///  
    ///     GET /profile
    ///     user's id - "asd11esd23"
    ///     
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    /// <response code="400">User not found</response>
    [Route("profile")]
    [HttpGet]
    public Task<IActionResult> GetProfile(string id)
    {
        var result = _profileService.GetProfile(id);

        return Task.FromResult<IActionResult>(Ok(result));
    }

    /// <summary>
    /// Returns user's id
    /// </summary>
    /// <remarks>
    ///  
    ///     GET /myId
    ///    
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [Route("myId")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMyId()
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        return Json(user.Id);
    }
    /// <summary>
    /// Returns profile picture of user
    /// </summary>
    /// <remarks>
    ///  
    ///     GET /profilePicture
    ///     user's id - "wqd12ed1332dfs"
    ///    
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    /// <response code="400">User not found</response>
    [HttpGet]
    [Route("profilePicture")]
    public async Task<IActionResult> GetProfilePicture(string userId)
    {
        var rawPicture = await _picturesDataService.GetUserProfilePictureAsync(userId);
        return File(rawPicture, "image/png");
    }
}