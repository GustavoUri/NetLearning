

namespace MeetApi.Models;

public class City
{
    public int id { get; set; }
    public string name { get; set; }

    public List<User> Users { get; set; }

}