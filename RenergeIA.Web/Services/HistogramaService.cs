using Microsoft.EntityFrameworkCore;
using RenergeIA.Core.Entities;
using RenergeIA.Core.Enums;
using RenergeIA.Infrastructure.Data;

namespace RenergeIA.Web.Services;

public class HistogramaService
{
    private readonly RenergeIADbContext _db;

    public HistogramaService(RenergeIADbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Obtiene la plantilla de histograma más cercana a la capacidad especificada
    /// </summary>
    public async Task<PlantillaHistograma?> ObtenerPlantillaPorCapacidadAsync(decimal capacidadMW, TipoHistograma tipo)
    {
        // Buscar plantilla exacta o la más cercana
        var plantillas = await _db.PlantillasHistogramas
            .Where(p => p.Tipo == tipo)
            .OrderBy(p => Math.Abs(p.CapacidadMW - capacidadMW))
            .ToListAsync();

        return plantillas.FirstOrDefault();
    }

    /// <summary>
    /// Obtiene todos los items de una plantilla
    /// </summary>
    public async Task<List<ItemHistograma>> ObtenerItemsPlantillaAsync(int plantillaId)
    {
        return await _db.ItemsHistograma
            .Where(i => i.PlantillaHistogramaId == plantillaId)
            .OrderBy(i => i.Cargo)
            .ThenBy(i => i.Actividad)
            .ToListAsync();
    }

    /// <summary>
    /// Calcula los totales por mes para una plantilla
    /// </summary>
    public async Task<Dictionary<int, decimal>> CalcularTotalesPorMesAsync(int plantillaId)
    {
        var items = await ObtenerItemsPlantillaAsync(plantillaId);

        var totales = new Dictionary<int, decimal>();

        for (int mes = 1; mes <= 12; mes++)
        {
            totales[mes] = items.Sum(i => ObtenerValorMes(i, mes));
        }

        return totales;
    }

    /// <summary>
    /// Genera datos para gráfico de área apilada
    /// </summary>
    public async Task<HistogramaGraficoData> GenerarDatosGraficoAsync(int plantillaId)
    {
        var items = await ObtenerItemsPlantillaAsync(plantillaId);

        // Agrupar por cargo principal (sin actividad)
        var porCargo = items
            .GroupBy(i => i.Cargo)
            .Select(g => new SerieHistograma
            {
                Nombre = g.Key,
                Valores = Enumerable.Range(1, 12)
                    .Select(mes => g.Sum(i => ObtenerValorMes(i, mes)))
                    .ToArray()
            })
            .ToList();

        return new HistogramaGraficoData
        {
            Labels = new[] { "Mes 1", "Mes 2", "Mes 3", "Mes 4", "Mes 5", "Mes 6",
                           "Mes 7", "Mes 8", "Mes 9", "Mes 10", "Mes 11", "Mes 12" },
            Series = porCargo
        };
    }

    /// <summary>
    /// Inicializa datos de ejemplo para demostración
    /// </summary>
    public async Task InicializarDatosEjemploAsync()
    {
        // Verificar si ya existen datos
        if (await _db.PlantillasHistogramas.AnyAsync())
            return;

        // Crear plantillas de ejemplo para 20MW
        var plantillaPersonal20MW = new PlantillaHistograma
        {
            Nombre = "Personal 20 MW",
            CapacidadMW = 20,
            Tipo = TipoHistograma.Personal,
            Descripcion = "Histograma de personal para proyecto fotovoltaico de 20 MW",
            FechaCreacion = DateTime.UtcNow
        };

        var plantillaEquipos20MW = new PlantillaHistograma
        {
            Nombre = "Equipos 20 MW",
            CapacidadMW = 20,
            Tipo = TipoHistograma.Equipos,
            Descripcion = "Histograma de equipos para proyecto fotovoltaico de 20 MW",
            FechaCreacion = DateTime.UtcNow
        };

        _db.PlantillasHistogramas.AddRange(plantillaPersonal20MW, plantillaEquipos20MW);
        await _db.SaveChangesAsync();

        // Items de personal de ejemplo (basados en los PDFs)
        var itemsPersonal = new List<ItemHistograma>
        {
            new() { PlantillaHistogramaId = plantillaPersonal20MW.Id, CodigoCargo = "C_ADP007", Cargo = "Administrador de Obra", NombreHistograma = "Administrador de obra", Actividad = "Staff de obra", TiempoMeses = 12, Mes1 = 1, Mes2 = 1, Mes3 = 1, Mes4 = 1, Mes5 = 1, Mes6 = 1, Mes7 = 1, Mes8 = 1, Mes9 = 1, Mes10 = 1, Mes11 = 1, Mes12 = 1, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaPersonal20MW.Id, CodigoCargo = "C_COP025", Cargo = "Ayudante de Obra", NombreHistograma = "Ayudante de obra", Actividad = "Mov Tierras", TiempoMeses = 6, Mes1 = 8, Mes2 = 8, Mes3 = 8, Mes4 = 8, Mes5 = 8, Mes6 = 8, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaPersonal20MW.Id, CodigoCargo = "C_COP025", Cargo = "Ayudante mecánico", NombreHistograma = "Ayudante mecánico", Actividad = "Hincado/prearmado", TiempoMeses = 3, Mes4 = 18, Mes5 = 18, Mes6 = 18, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaPersonal20MW.Id, CodigoCargo = "C_COP025", Cargo = "Ayudante eléctrico", NombreHistograma = "Ayudante eléctrico", Actividad = "Instalación eléctrica", TiempoMeses = 6, Mes5 = 7, Mes6 = 13, Mes7 = 13, Mes8 = 13, Mes9 = 13, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaPersonal20MW.Id, CodigoCargo = "C_COP022", Cargo = "Técnico Civil", NombreHistograma = "Oficial civil", Actividad = "Obra", TiempoMeses = 6, Mes1 = 2, Mes2 = 2, Mes3 = 5, Mes4 = 5, Mes5 = 5, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaPersonal20MW.Id, CodigoCargo = "C_COP021", Cargo = "Técnico Electricista", NombreHistograma = "Técnico eléctrico", Actividad = "Instalación eléctrica", TiempoMeses = 6, Mes5 = 2, Mes6 = 6, Mes7 = 6, Mes8 = 6, Mes9 = 2, FechaCreacion = DateTime.UtcNow },
        };

        // Items de equipos de ejemplo
        var itemsEquipos = new List<ItemHistograma>
        {
            new() { PlantillaHistogramaId = plantillaEquipos20MW.Id, CodigoCargo = "", Cargo = "Motoniveladora", NombreHistograma = "", Actividad = "Movimiento de tierras y vía de acceso", TiempoMeses = 4, Mes1 = 1, Mes2 = 1, Mes3 = 1, Mes4 = 1, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaEquipos20MW.Id, CodigoCargo = "", Cargo = "Retroexcavadora", NombreHistograma = "", Actividad = "Movimiento de tierras", TiempoMeses = 6, Mes1 = 1, Mes2 = 1, Mes3 = 1, Mes4 = 1, Mes5 = 1, Mes6 = 1, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaEquipos20MW.Id, CodigoCargo = "", Cargo = "Hincadora", NombreHistograma = "", Actividad = "Hincado directo", TiempoMeses = 2, Mes4 = 2, Mes5 = 2, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaEquipos20MW.Id, CodigoCargo = "", Cargo = "Generador 30 kVA", NombreHistograma = "", Actividad = "Energía de contenedores", TiempoMeses = 12, Mes1 = 1, Mes2 = 1, Mes3 = 1, Mes4 = 1, Mes5 = 1, Mes6 = 1, Mes7 = 1, Mes8 = 1, Mes9 = 1, Mes10 = 1, Mes11 = 1, Mes12 = 1, FechaCreacion = DateTime.UtcNow },
            new() { PlantillaHistogramaId = plantillaEquipos20MW.Id, CodigoCargo = "", Cargo = "Camioneta fija 4X4", NombreHistograma = "", Actividad = "Apoyo interno y SST", TiempoMeses = 12, Mes1 = 1, Mes2 = 1, Mes3 = 1, Mes4 = 1, Mes5 = 1, Mes6 = 1, Mes7 = 1, Mes8 = 1, Mes9 = 1, Mes10 = 1, Mes11 = 1, Mes12 = 1, FechaCreacion = DateTime.UtcNow },
        };

        _db.ItemsHistograma.AddRange(itemsPersonal);
        _db.ItemsHistograma.AddRange(itemsEquipos);
        await _db.SaveChangesAsync();
    }

    private decimal ObtenerValorMes(ItemHistograma item, int mes)
    {
        return mes switch
        {
            1 => item.Mes1,
            2 => item.Mes2,
            3 => item.Mes3,
            4 => item.Mes4,
            5 => item.Mes5,
            6 => item.Mes6,
            7 => item.Mes7,
            8 => item.Mes8,
            9 => item.Mes9,
            10 => item.Mes10,
            11 => item.Mes11,
            12 => item.Mes12,
            _ => 0
        };
    }
}

public class HistogramaGraficoData
{
    public string[] Labels { get; set; } = Array.Empty<string>();
    public List<SerieHistograma> Series { get; set; } = new();
}

public class SerieHistograma
{
    public string Nombre { get; set; } = string.Empty;
    public decimal[] Valores { get; set; } = Array.Empty<decimal>();
}
