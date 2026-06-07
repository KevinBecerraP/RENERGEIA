namespace RenergeIA.Core.Entities;

public class RegistroHorometro : EntidadBase
{
    public int EquipoId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal LecturaHorometro { get; set; }
    public decimal HorasTrabajadas { get; set; }
    public string Operador { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;

    public Equipo Equipo { get; set; } = null!;
}
