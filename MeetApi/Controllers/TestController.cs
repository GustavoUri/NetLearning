using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    // GET
    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        return Content("Ok,man");
    }
}