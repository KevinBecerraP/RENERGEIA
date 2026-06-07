using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class ActividadWBS : EntidadBase
{
    public int ProyectoId { get; set; }
    public int? ActividadPadreId { get; set; }
    public string CodigoWBS { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int NivelWBS { get; set; }
    public string Responsable { get; set; } = string.Empty;
    public DateTime FechaInicioPlanificada { get; set; }
    public DateTime FechaFinPlanificada { get; set; }
    public DateTime? FechaInicioReal { get; set; }
    public DateTime? FechaFinReal { get; set; }
    public decimal AvancePlanificado { get; set; }
    public decimal AvanceReal { get; set; }
    public EstadoActividad Estado { get; set; } = EstadoActividad.Pendiente;
    public bool Activo { get; set; } = true;

    public Proyecto Proyecto { get; set; } = null!;
    public ActividadWBS? ActividadPadre { get; set; }
    public ICollection<ActividadWBS> SubActividades { get; set; } = new List<ActividadWBS>();
    public ICollection<RegistroAvanceDiario> RegistrosAvance { get; set; } = new List<RegistroAvanceDiario>();
}
