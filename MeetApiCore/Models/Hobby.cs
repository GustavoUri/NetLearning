using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Hobby
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }

    public List<User> Users { get; set; }
}