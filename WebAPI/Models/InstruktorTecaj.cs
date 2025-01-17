namespace OnlineITCourses.Models;

public class InstruktorTecaj
{
    public int InstruktorId { get; set; }
    public Instruktor? Instruktor { get; set; }
    public int TecajId { get; set; }
    public Tecaj? Tecaj { get; set; }
}