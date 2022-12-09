using Entities.Data;
using Entities.Exceptions;
using Entities.Interfaces.Services;
using Entities.Models;

namespace Entities.Services;

public class HobbiesDataService : IHobbiesDataService
{
    private readonly AppDbContext _db;

    public HobbiesDataService(AppDbContext db)
    {
        _db = db;
    }

    public List<Hobby> GetAllHobbies()
    {
        var result = _db.Hobbies.ToList();
        return result;
    }

    public Hobby GetHobbyById(int id)
    {
        var hobbies = GetAllHobbies();
        var result = hobbies.FirstOrDefault(hobby => hobby.Id == id);
        if (result == null)
            throw new BadRequestException("There is no hobby with this id");
        return result;
    }

    public Hobby GetHobbyByName(string name)
    {
        var hobbies = GetAllHobbies();
        var result = hobbies.FirstOrDefault(hobby => hobby.Name == name);
        if (result == null)
            throw new BadRequestException("There is no city with this name");
        return result;
    }

    public void AddHobby(string hobbyName)
    {
        var hobby = new Hobby()
        {
            Name = hobbyName
        };
        _db.Hobbies.Add(hobby);
        _db.SaveChanges();
    }

    public List<string> GetHobbiesByBeginningToUser(string beginning)
    {
        var hobbies = GetAllHobbies();
        var result = hobbies
            .Where(hobby => hobby.Name.ToLower().Contains(beginning.ToLower())).Select(hobby => hobby.Name)
            .ToList();
        return result;
    }
}