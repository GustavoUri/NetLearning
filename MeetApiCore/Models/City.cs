

using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class City
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    //public List<User>? Users { get; set; }

}