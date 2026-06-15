using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class PlantillaHistograma : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public decimal CapacidadMW { get; set; }
    public TipoHistograma Tipo { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    // Relaciones
    public ICollection<ItemHistograma> Items { get; set; } = new List<ItemHistograma>();
}
