using MeetApi.Models;

namespace MeetApi.Interfaces.Services;

public interface IDataService
{
    public List<User> GetAllUsers();
    public User GetUserById(string id);
    public User GetUserByUserName(string userName);
    public List<Message> GetAllMessages();
    public Message GetMessageById(int id);
    public List<Chat> GetAllChats();
    public Chat GetChatById(int id);
    public List<City> GetAllCities();
    public City GetCityById(int id);
    public List<Hobby> GetAllHobbies();
    public Hobby GetHobbyById(int id);
    public Hobby GetHobbyByName(string name);
    public void SaveChanges();

}