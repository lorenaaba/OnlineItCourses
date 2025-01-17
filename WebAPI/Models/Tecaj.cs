using System.Text.RegularExpressions;

namespace OnlineITCourses.Models;

public class Tecaj
{
    public int Id { get; set; }
    public string Naslov { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public DateTime DatumPocetka { get; set; }
    public decimal? Cijena { get; set; }
    public int BrojPrijavljenih { get; set; }
    public int GrupaId { get; set; }
    public Grupa Grupa { get; set; }
    public ICollection<InstruktorTecaj>? Instruktori{ get; set; }
    
}