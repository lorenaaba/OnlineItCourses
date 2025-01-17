namespace OnlineITCourses.Models;

public class Prijava
{
    public int Id { get; set; }
    public int KorisnikId { get; set; }
    public int TecajId { get; set; }
    public DateTime DatumPrijave { get; set; }
}