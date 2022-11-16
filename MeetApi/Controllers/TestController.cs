using MeetApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    // GET
    [HttpGet]
    public async Task<IActionResult> AddProfileForm()
    {
        string path = $"Pictures/1116bedc-0bf6-4370-b422-6164ca8bc086";
        var fileStream = new MemoryStream(System.IO.File.ReadAllBytes(path));
        var file = new FormFile(fileStream, 0, fileStream.Length, "g", path);
        return Json(file);
    }
}