using Entities.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetApi.Controllers;
[ApiController]
public class CitiesController : Controller
{
    private readonly ICitiesDataService _citiesDataService;

    public CitiesController(ICitiesDataService citiesDataService)
    {
        _citiesDataService = citiesDataService;
    }
    /// <summary>
    /// Returns all cities
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     GET /cities
    ///     
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [HttpGet]
    [Route("cities")]
    public IActionResult GetCities()
    {
        var cities = _citiesDataService.GetAllCities();
        return Json(cities);
    }
    
    /// <summary>
    /// Returns cities by beginning
    /// </summary>
    /// <remarks>
    /// 
    ///     
    ///     
    ///  
    ///     GET /cities
    ///     city name beginning - "Ekat"
    ///    
    /// 
    /// </remarks>
    /// <response code="200">Successful</response>
    /// <response code="500">Server problem</response>
    [HttpGet]
    [Route("cityByBeginning")]
    public IActionResult GetCityByName(string beginning)
    {
        var cities = _citiesDataService.GetCitiesByBeginningToUser(beginning);
        return Json(cities);
    }
}