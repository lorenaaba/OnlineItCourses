namespace OnlineITCourses.Models;

public class Instruktor
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Email { get; set; }
    public ICollection<InstruktorTecaj>? Tecaj { get; set; }
}