using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class Documento : EntidadBase
{
    public int ProyectoId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public TipoDocumento TipoDocumento { get; set; }
    public EstadoDocumento Estado { get; set; } = EstadoDocumento.Borrador;
    public string Disciplina { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }

    public Proyecto Proyecto { get; set; } = null!;
    public ICollection<VersionDocumento> Versiones { get; set; } = new List<VersionDocumento>();
}
