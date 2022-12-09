using Entities.Models;

namespace Entities.Interfaces.Services;

public interface IHobbiesDataService
{
    public List<Hobby> GetAllHobbies();
    public Hobby GetHobbyById(int id);
    public Hobby GetHobbyByName(string name);
    public void AddHobby(string hobbyName);
    public List<string> GetHobbiesByBeginningToUser(string beginning);
}