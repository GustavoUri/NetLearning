using MeetApi.Data;
using MeetApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;

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
        private readonly AppDbContext _db;
        private readonly IRegistrationService _registrationService;
        private readonly IAuthentificationService _authentificationService;
        public AuthorizationController(UserManager<User> userManager, 
            SignInManager<User> signInManager, AppDbContext db, IRegistrationService registrationService, IAuthentificationService authentificationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _registrationService = registrationService;
            _authentificationService = authentificationService;

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
                await _registrationService.RegisterAsync(model);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogInto(LoginToServer model)
        {
            if (ModelState.IsValid)
            {
                await _authentificationService.LoginAsync(model);
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