using System.Net;
using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MeetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    // GET
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> AddProfileForm(HubConnectionContext connection)
    {
        string info = connection.UserIdentifier;
        return Json(info);
    }
}