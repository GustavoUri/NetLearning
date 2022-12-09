using Entities.Interfaces.Services;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MeetApi.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IRegistrationService registrationService, IAuthenticationService authenticationService)
        {
            _registrationService = registrationService;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Performs registration
        /// </summary>
        /// <remarks>
        /// Note that:
        /// Login must be from 3 to 20 characters,
        /// password must be from 5 to 25 characters
        /// and password must have numbers and special symbols,
        /// when you make registration server logs in automatically
        /// 
        ///     
        ///     
        ///  
        ///     POST /register
        ///     {
        ///     "login": "ural1",
        ///     "password": "Qisdvd13@3"
        ///     }
        ///    
        /// 
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="200">Registration was successful</response>
        /// <response code="400">Wrong form or login already taken</response>
        /// <response code="500">Server problem</response>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            await _registrationService.RegisterAsync(model);
            return Ok();
        }

        /// <summary>
        /// Performs authentication
        /// </summary>
        /// <remarks>
        /// Note that:
        /// "rememberMe": true - is used for long cookies saving
        /// 
        ///     
        ///     
        ///  
        ///     POST /login
        ///     {
        ///     "login": "ural1",
        ///     "password": "Qisdvd13@3",
        ///     "rememberMe": true
        ///     }
        /// 
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="200">Authentication was successful</response>
        /// <response code="400">Wrong login or password</response>
        /// <response code="500">Server problem</response>
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogInto(LoginToServer model)
        {
            await _authenticationService.LoginAsync(model);
            return Ok();
        }

        /// <summary>
        /// Logs out of the account
        /// </summary>
        /// <remarks>
        /// POST /logout
        /// </remarks>
        /// <response code="200">Logout was successful</response>
        /// <response code="500">Server problem</response>
        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
            return Ok();
        }
    }
}