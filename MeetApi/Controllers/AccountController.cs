using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppContext _db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User {UserName = model.Login};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (!result.Succeeded)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize]
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [Route("Profile")]
        [HttpPost]
        public async Task<IActionResult> AddProfileForm(ProfileFormModel formModel)
        {
            User user = await _userManager.FindByNameAsync(User.Identity?.Name);
            user.UpdateUser(formModel);
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        
        [Authorize]
        [Route("ProfilePhoto")]
        [HttpPost]
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

        // [Route("ProfilePhoto")]
        // [HttpGet]
        // public Task<IActionResult> GetProfilePhoto(string id)
        // {
        //     var user = _db.Users.First(x => x.Id == id);
        //     var result = $"~/{user.PhotoPath}";
        //     return Task.FromResult<IActionResult>(Content(result));
        // }
    }
}