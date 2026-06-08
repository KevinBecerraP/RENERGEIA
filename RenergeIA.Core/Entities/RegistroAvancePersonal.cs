namespace RenergeIA.Core.Entities;

public class RegistroAvancePersonal : EntidadBase
{
    public int RegistroAvanceDiarioId { get; set; }
    public int PersonalProyectoId { get; set; }
    public decimal HorasTrabajadas { get; set; }

    public RegistroAvanceDiario RegistroAvanceDiario { get; set; } = null!;
    public PersonalProyecto PersonalProyecto { get; set; } = null!;
}
