using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class RegistroAvanceDiario : EntidadBase
{
    public int ProyectoId { get; set; }
    public int ActividadWBSId { get; set; }
    public int InformeDiarioId { get; set; }
    public DateTime Fecha { get; set; }

    // Avance y cantidad
    public decimal CantidadEjecutadaDia { get; set; }
    public decimal PorcentajeAvance { get; set; }
    public decimal AvanceEsperado { get; set; }
    public decimal AvanceAcumulado { get; set; }
    public decimal Desviacion { get; set; }
    public int DiasAtraso { get; set; }
    public EstadoAvance Estado { get; set; } = EstadoAvance.AlDia;

    // Información complementaria
    public decimal HorasTrabajadas { get; set; }
    public int PersonalEnSitio { get; set; }
    public decimal? HorasAfectadasClima { get; set; }
    public string? Novedades { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public string ReportadoPor { get; set; } = string.Empty;

    public Proyecto Proyecto { get; set; } = null!;
    public ActividadWBS ActividadWBS { get; set; } = null!;
    public InformeDiario InformeDiario { get; set; } = null!;
}
