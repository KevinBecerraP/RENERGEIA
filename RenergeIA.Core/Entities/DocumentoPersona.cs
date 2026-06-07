namespace RenergeIA.Core.Entities;

public class DocumentoPersona : EntidadBase
{
    public int PersonalProyectoId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NombreDocumento { get; set; } = string.Empty;
    public string RutaArchivo { get; set; } = string.Empty;
    public DateTime? FechaEmision { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public bool EstaVigente => FechaVencimiento == null || FechaVencimiento > DateTime.Today;

    public PersonalProyecto Personal { get; set; } = null!;
}
