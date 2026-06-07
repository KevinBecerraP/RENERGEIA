using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class Proyecto : EntidadBase
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public decimal CapacidadKWp { get; set; }
    public decimal PresupuestoContractual { get; set; }
    public DateTime FechaInicioPlanificada { get; set; }
    public DateTime FechaFinPlanificada { get; set; }
    public DateTime? FechaInicioReal { get; set; }
    public DateTime? FechaFinReal { get; set; }
    public EstadoProyecto Estado { get; set; } = EstadoProyecto.Planificacion;
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<ActividadWBS> Actividades { get; set; } = new List<ActividadWBS>();
    public ICollection<InformeDiario> InformesDiarios { get; set; } = new List<InformeDiario>();
    public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    public ICollection<Partida> Partidas { get; set; } = new List<Partida>();
    public ICollection<NoConformidad> NoConformidades { get; set; } = new List<NoConformidad>();
    public ICollection<Restriccion> Restricciones { get; set; } = new List<Restriccion>();
    public ICollection<PersonalProyecto> Personal { get; set; } = new List<PersonalProyecto>();
    public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
    public ICollection<RegistroClima> RegistrosClima { get; set; } = new List<RegistroClima>();
}
