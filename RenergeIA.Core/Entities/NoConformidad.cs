using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class NoConformidad : EntidadBase
{
    public int ProyectoId { get; set; }
    public string Numero { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public SeveridadNoConformidad Severidad { get; set; }
    public EstadoNoConformidad Estado { get; set; } = EstadoNoConformidad.Abierta;
    public DateTime FechaDeteccion { get; set; }
    public DateTime? FechaCierre { get; set; }
    public string DetectadoPor { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;

    public Proyecto Proyecto { get; set; } = null!;
    public ICollection<AccionCorrectiva> AccionesCorrectivas { get; set; } = new List<AccionCorrectiva>();
}
