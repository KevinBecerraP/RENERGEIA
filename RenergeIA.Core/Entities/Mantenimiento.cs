using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class Mantenimiento : EntidadBase
{
    public int EquipoId { get; set; }
    public TipoMantenimiento TipoMantenimiento { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public string RealizadoPor { get; set; } = string.Empty;
    public DateTime? ProximoMantenimiento { get; set; }
    public string Observaciones { get; set; } = string.Empty;

    public Equipo Equipo { get; set; } = null!;
}
