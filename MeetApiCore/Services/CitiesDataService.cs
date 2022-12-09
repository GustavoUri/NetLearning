using Entities.Data;
using Entities.Exceptions;
using Entities.Interfaces.Services;
using Entities.Models;

namespace Entities.Services;

public class CitiesDataService : ICitiesDataService
{
    private readonly AppDbContext _db;

    public CitiesDataService(AppDbContext db)
    {
        _db = db;
    }
    public List<City> GetAllCities()
    {
        var result = _db.Cities.ToList();
        return result;
    }

    public City GetCityById(int id)
    {
        var cities = GetAllCities();
        var result = cities.FirstOrDefault(city => city.Id == id);
        if (result == null)
            throw new BadRequestException("There is no city with this id");
        return result;
    }

    public City GetCityByName(string name)
    {
        var cities = GetAllCities();
        var result = cities.FirstOrDefault(city => city.Name == name);
        if (result == null)
            throw new BadRequestException("There is no city with this name");
        return result;
    }

    public void AddCity(string cityName)
    {
        var city = new City()
        {
            Name = cityName
        };
        _db.Cities.Add(city);
        _db.SaveChanges();
    }

    public List<string> GetCitiesByBeginningToUser(string beginning)
    {
        var cities = GetAllCities();
        var result = cities
            .Where(city => city.Name.ToLower().Contains(beginning.ToLower())).Select(city => city.Name)
            .ToList();
        return result;
    }
}