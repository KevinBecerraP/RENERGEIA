using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class Equipo : EntidadBase
{
    public int ProyectoId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public TipoEquipo TipoEquipo { get; set; }
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Propietario { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }
    public DateTime? FechaSalida { get; set; }
    public bool Activo { get; set; } = true;

    public Proyecto Proyecto { get; set; } = null!;
    public ICollection<RegistroHorometro> RegistrosHorometro { get; set; } = new List<RegistroHorometro>();
    public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
