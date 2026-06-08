namespace RenergeIA.Core.Entities;

public class RegistroAvanceRestriccion : EntidadBase
{
    public int RegistroAvanceDiarioId { get; set; }
    public int RestriccionId { get; set; }

    public RegistroAvanceDiario RegistroAvanceDiario { get; set; } = null!;
    public Restriccion Restriccion { get; set; } = null!;
}
