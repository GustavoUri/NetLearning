using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

    [Route("getProfile")]
    [HttpGet]
    public async Task<IActionResult> GetProfile(string id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user != null)
        {
            var profile = new ProfileForm();
            profile.UpdateForm(user);
            return Json(profile);
        }

        return BadRequest();
    }


    [Route("getMyId")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMyId()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        return Json(user.Id);
    }
    
    [HttpGet]
    [Route("getImage")]
    public async Task<IActionResult> GetImage(string id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user?.PhotoPath == null)
            return BadRequest();
        var path = user.PhotoPath;
        var b = await System.IO.File.ReadAllBytesAsync(path);
        return File(b, "image/png");
    }

    // [Route("getPhoto")]
    // [HttpGet]
    // [Authorize]
    // public async Task<IActionResult> GetPhoto(string id)
    // {
    //     var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
    //     if (user != null)
    //         return Json(user.PhotoPath);
    //     return BadRequest();
    // }
    
    [Route("getCities")]
    [HttpGet]
    public async Task<IActionResult> GetCities(string cityNameBeginning)
    {
        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        cityNameBeginning = ti.ToTitleCase(cityNameBeginning);
            var cities = _db.Cities.AsParallel().Where(x => x.c1.StartsWith(cityNameBeginning)).Select(city => city.c1).ToList();
            return Json(cities);
    }
    
    [Route("getHobbies")]
    [HttpGet]
    public IActionResult GetHobbies()
    {
        var ti = CultureInfo.CurrentCulture.TextInfo;
        var hobbies = _db.Hobbies.Select(hobby => hobby.Name).ToList();
        return Json(hobbies);
    }
}
