using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.Controllers
{
    /// <summary>
    /// Used for authorization
    /// </summary>
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppContext _db;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, AppContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        /// <summary>
        /// Register in app
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
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


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogInto(UserLogin model)
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
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}