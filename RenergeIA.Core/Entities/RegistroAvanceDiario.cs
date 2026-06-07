namespace RenergeIA.Core.Entities;

public class RegistroAvanceDiario : EntidadBase
{
    public int ProyectoId { get; set; }
    public int ActividadWBSId { get; set; }
    public int InformeDiarioId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal PorcentajeAvance { get; set; }
    public decimal HorasTrabajadas { get; set; }
    public int PersonalEnSitio { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public string ReportadoPor { get; set; } = string.Empty;

    public Proyecto Proyecto { get; set; } = null!;
    public ActividadWBS ActividadWBS { get; set; } = null!;
    public InformeDiario InformeDiario { get; set; } = null!;
}
