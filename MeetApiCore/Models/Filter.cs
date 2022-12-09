

namespace Entities.Models;

public class Filter 
{
    public int FirstAgeBorder { get; set; }
    public int SecondAgeBorder { get; set; }
    public List<string>? Locations { get; set; }
    public string? Gender { get; set; }
    public List<string>? Hobbies { get; set; }
}