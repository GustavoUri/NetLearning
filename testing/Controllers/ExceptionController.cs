using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers;

public class ExceptionController : Controller
{
    [HttpGet("Index")]
    public IActionResult Index()
    {
        return Content("asd");
    }
}