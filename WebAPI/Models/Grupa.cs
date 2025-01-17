namespace OnlineITCourses.Models;

public class Grupa
{
    public int Id { get; set; }
    public string Naziv { get; set; }
    public ICollection<Tecaj> Tecajevi { get; set; }
}