namespace RenergeIA.Core.Entities;

public class RegistroAvanceEquipo : EntidadBase
{
    public int RegistroAvanceDiarioId { get; set; }
    public int EquipoId { get; set; }
    public decimal HorasUtilizadas { get; set; }

    public RegistroAvanceDiario RegistroAvanceDiario { get; set; } = null!;
    public Equipo Equipo { get; set; } = null!;
}
