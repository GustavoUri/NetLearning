using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.Controllers;

[ApiController]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AppContext _db;

    public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, AppContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }
    
    [Authorize]
    [Route("updateProfile")]
    [HttpPut]
    public async Task<IActionResult> AddProfileForm(ProfileForm form)
    {
        User user = await _userManager.FindByNameAsync(User.Identity?.Name);
        user.UpdateUser(form);
        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [Authorize]
    [Route("updatePhoto")]
    [HttpPut]
    public async Task<IActionResult> AddProfilePhoto(IFormFile? photo)
    {
        if (photo != null)
        {
            User user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var type = photo.ContentType.Split("/")[1];
            var path = $"Pictures/{user.Id}.{type}";
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            user.PhotoPath = path;
            await _userManager.UpdateAsync(user);
        }

        return Ok();
    }
}