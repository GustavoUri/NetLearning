using Entities.Models;

namespace Entities.Interfaces.Services;

public interface ICitiesDataService
{
    public List<City> GetAllCities();
    public City GetCityById(int id);
    public City GetCityByName(string name);
    public void AddCity(string cityName);
    public List<string> GetCitiesByBeginningToUser(string beginning);
}