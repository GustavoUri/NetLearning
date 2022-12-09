using Entities.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetApi.Controllers;

public class HobbiesController : Controller
{
    private readonly IHobbiesDataService _hobbiesDataService;

    public HobbiesController(IHobbiesDataService hobbiesDataService)
    {
        _hobbiesDataService = hobbiesDataService;
    }

    /// <summary>
    /// Returns all hobbies
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     GET /hobbies
    ///     
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [HttpGet]
    [Route("hobbies")]
    public IActionResult GetHobbies()
    {
        var hobbies = _hobbiesDataService.GetAllHobbies();
        return Json(hobbies);
    }

    /// <summary>
    /// Returns hobby by beginning
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     GET /hobbyByBeginning
    ///     hobby name beginning - "footb"
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [HttpGet]
    [Route("hobbyByBeginning")]
    public IActionResult GetHobbyByBeginning(string beginning)
    {
        var hobbies = _hobbiesDataService.GetHobbiesByBeginningToUser(beginning);
        return Json(hobbies);
    }
}