using System.Net;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MeetApi.SignalRContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppContext = MeetApi.Models.AppContext;

namespace MeetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    // GET
    //[Authorize]
    [HttpGet]
    public async Task<IActionResult> AddProfileForm()
    {
        var x = new AppContext();
        var user = x.Users.First(x => x.UserName == User.Identity.Name);
        
        return Json(user.BlockedUsersId);
    }
}