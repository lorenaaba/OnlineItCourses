using OnlineITCourses.Models;

namespace OnlineITCourses.Dto;

public class TecajDto
{
    public int Id { get; set; }
    public string Naslov { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public DateTime DatumPocetka { get; set; }
    public decimal Cijena { get; set; }
    public int GrupaId { get; set; }
    public Grupa Grupa { get; set; } 
}