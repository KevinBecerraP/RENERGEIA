namespace RenergeIA.Core.Entities;

public class Fotografia : EntidadBase
{
    public int InformeDiarioId { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public string RutaArchivo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaToma { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public string Etiquetas { get; set; } = string.Empty;

    public InformeDiario InformeDiario { get; set; } = null!;
}
