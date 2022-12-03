using System.Net;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MeetApi.Data;
using MeetApi.Interfaces.Services;
using MeetApi.Services;
using MeetApi.SignalRContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    private readonly IDataService _dataService;
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly IProfileService _profileService;
    public TestController(IDataService dataService, AppDbContext db, UserManager<User> userManager, IProfileService profileService)
    {
        _dataService = dataService;
        _db = db;
        _userManager = userManager;
        _profileService = profileService;
    }
    
    // GET
    //[Authorize]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> AddProfileForm()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        return Json(_dataService.GetAllUsers());
    }
}