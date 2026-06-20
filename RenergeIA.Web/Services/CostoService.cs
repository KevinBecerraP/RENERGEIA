using Microsoft.EntityFrameworkCore;
using RenergeIA.Core.Entities;
using RenergeIA.Infrastructure.Data;

namespace RenergeIA.Web.Services;

public class CostoService
{
    private readonly RenergeIADbContext _db;

    public CostoService(RenergeIADbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Obtiene todas las partidas de un proyecto con sus costos reales
    /// </summary>
    public async Task<List<Partida>> ObtenerPartidasConCostosAsync(int proyectoId)
    {
        return await _db.Partidas
            .Include(p => p.CostosReales)
            .Include(p => p.ActividadWBS)
            .Where(p => p.ProyectoId == proyectoId)
            .OrderBy(p => p.Codigo)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene costos reales de una partida
    /// </summary>
    public async Task<List<CostoReal>> ObtenerCostosRealesAsync(int partidaId)
    {
        return await _db.CostosReales
            .Where(c => c.PartidaId == partidaId)
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();
    }

    /// <summary>
    /// Calcula el total ejecutado de una partida
    /// </summary>
    public async Task<decimal> CalcularTotalEjecutadoAsync(int partidaId)
    {
        return await _db.CostosReales
            .Where(c => c.PartidaId == partidaId)
            .SumAsync(c => c.Monto);
    }

    /// <summary>
    /// Calcula resumen de costos del proyecto
    /// </summary>
    public async Task<ResumenCostos> ObtenerResumenCostosAsync(int proyectoId)
    {
        var partidas = await ObtenerPartidasConCostosAsync(proyectoId);

        var totalPresupuestado = partidas.Sum(p => p.MontoPresupuestado);
        var totalEjecutado = partidas.Sum(p => p.CostosReales.Sum(c => c.Monto));
        var diferencia = totalPresupuestado - totalEjecutado;
        var porcentajeEjecutado = totalPresupuestado > 0
            ? (totalEjecutado / totalPresupuestado) * 100
            : 0;

        return new ResumenCostos
        {
            TotalPresupuestado = totalPresupuestado,
            TotalEjecutado = totalEjecutado,
            Diferencia = diferencia,
            PorcentajeEjecutado = porcentajeEjecutado,
            TotalPartidas = partidas.Count,
            PartidasConCostos = partidas.Count(p => p.CostosReales.Any())
        };
    }

    /// <summary>
    /// Obtiene costos agrupados por categoría
    /// </summary>
    public async Task<List<CostoPorCategoria>> ObtenerCostosPorCategoriaAsync(int proyectoId)
    {
        var partidas = await ObtenerPartidasConCostosAsync(proyectoId);

        return partidas
            .GroupBy(p => string.IsNullOrEmpty(p.Categoria) ? "Sin categoría" : p.Categoria)
            .Select(g => new CostoPorCategoria
            {
                Categoria = g.Key,
                Presupuestado = g.Sum(p => p.MontoPresupuestado),
                Ejecutado = g.Sum(p => p.CostosReales.Sum(c => c.Monto)),
                CantidadPartidas = g.Count()
            })
            .OrderByDescending(c => c.Presupuestado)
            .ToList();
    }

    /// <summary>
    /// Crea partidas de ejemplo para un proyecto
    /// </summary>
    public async Task CrearPartidasEjemploAsync(int proyectoId)
    {
        // Verificar si ya existen partidas
        if (await _db.Partidas.AnyAsync(p => p.ProyectoId == proyectoId))
            return;

        var partidas = new List<Partida>
        {
            new() { ProyectoId = proyectoId, Codigo = "MT-001", Descripcion = "Movimiento de tierras", Unidad = "m3", Categoria = "Movimiento de Tierras", CantidadPresupuestada = 500, PrecioUnitario = 15000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "MT-002", Descripcion = "Compactación de suelo", Unidad = "m2", Categoria = "Movimiento de Tierras", CantidadPresupuestada = 2000, PrecioUnitario = 8000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "CV-001", Descripcion = "Cimentación estructuras", Unidad = "m3", Categoria = "Obra Civil", CantidadPresupuestada = 150, PrecioUnitario = 350000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "CV-002", Descripcion = "Zanjas para cableado", Unidad = "ml", Categoria = "Obra Civil", CantidadPresupuestada = 3000, PrecioUnitario = 12000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "MC-001", Descripcion = "Estructuras metálicas", Unidad = "ton", Categoria = "Mecánica", CantidadPresupuestada = 80, PrecioUnitario = 4500000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "MC-002", Descripcion = "Hincado de postes", Unidad = "und", Categoria = "Mecánica", CantidadPresupuestada = 1200, PrecioUnitario = 85000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "EL-001", Descripcion = "Paneles solares 550W", Unidad = "und", Categoria = "Eléctrica", CantidadPresupuestada = 36000, PrecioUnitario = 250000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "EL-002", Descripcion = "Inversores centrales", Unidad = "und", Categoria = "Eléctrica", CantidadPresupuestada = 10, PrecioUnitario = 180000000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "EL-003", Descripcion = "Cableado DC", Unidad = "ml", Categoria = "Eléctrica", CantidadPresupuestada = 15000, PrecioUnitario = 45000, FechaCreacion = DateTime.UtcNow },
            new() { ProyectoId = proyectoId, Codigo = "EL-004", Descripcion = "Transformadores", Unidad = "und", Categoria = "Eléctrica", CantidadPresupuestada = 5, PrecioUnitario = 95000000, FechaCreacion = DateTime.UtcNow },
        };

        _db.Partidas.AddRange(partidas);
        await _db.SaveChangesAsync();
    }
}

public class ResumenCostos
{
    public decimal TotalPresupuestado { get; set; }
    public decimal TotalEjecutado { get; set; }
    public decimal Diferencia { get; set; }
    public decimal PorcentajeEjecutado { get; set; }
    public int TotalPartidas { get; set; }
    public int PartidasConCostos { get; set; }
}

public class CostoPorCategoria
{
    public string Categoria { get; set; } = string.Empty;
    public decimal Presupuestado { get; set; }
    public decimal Ejecutado { get; set; }
    public int CantidadPartidas { get; set; }
    public decimal Diferencia => Presupuestado - Ejecutado;
    public decimal PorcentajeEjecutado => Presupuestado > 0 ? (Ejecutado / Presupuestado) * 100 : 0;
}
