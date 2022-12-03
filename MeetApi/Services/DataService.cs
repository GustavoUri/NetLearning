using MeetApi.Data;
using MeetApi.Interfaces.Services;
using MeetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.Services;

public class DataService : IDataService
{
    private readonly AppDbContext _db;

    public DataService(AppDbContext db)
    {
        _db = db;
    }

    public List<User> GetAllUsers()
    {
        var result = _db.Users
            .Include(user => user.Chats)
            .Include(user => user.Hobbies)
            .Include(user => user.Location)
            .Include(user => user.BlockedUsers).ToList();
        return result;
    }

    public User GetUserById(string id)
    {
        var result = GetAllUsers().FirstOrDefault(user => user.Id == id);
        return result;
    }

    public User GetUserByUserName(string userName)
    {
        var result = GetAllUsers().FirstOrDefault(user => user.UserName == userName);
        return result;
    }

    public List<Message> GetAllMessages()
    {
        var result = _db.Messages
            .Include(message => message.Chat)
            .Include(message => message.Receiver)
            .Include(message => message.Sender).ToList();
        return result;
    }

    public Message GetMessageById(int id)
    {
        var result = GetAllMessages().FirstOrDefault(message => message.Id == id);
        return result;
    }

    public List<Chat> GetAllChats()
    {
        var result = _db.Chats
            .Include(chat => chat.Messages)
            .Include(chat => chat.Users).ToList();
        return result;
    }

    public Chat GetChatById(int id)
    {
        var result = GetAllChats().FirstOrDefault(chat => chat.Id == id);
        return result;
    }

    public List<City> GetAllCities()
    {
        var result = _db.Cities.ToList();
        return result;
    }

    public City GetCityById(int id)
    {
        var result = GetAllCities().FirstOrDefault(city => city.id == id);
        return result;
    }

    public List<Hobby> GetAllHobbies()
    {
        var result = _db.Hobbies.ToList();
        return result;
    }

    public Hobby GetHobbyById(int id)
    {
        var result = GetAllHobbies().FirstOrDefault(hobby => hobby.Id == id);
        return result;
    }

    public Hobby GetHobbyByName(string name)
    {
        var result = GetAllHobbies().FirstOrDefault(hobby => hobby.Name == name);
        return result;
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}