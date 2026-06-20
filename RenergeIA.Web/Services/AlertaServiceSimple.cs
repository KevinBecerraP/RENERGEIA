using Microsoft.EntityFrameworkCore;
using RenergeIA.Core.Entities;
using RenergeIA.Core.Enums;
using RenergeIA.Infrastructure.Data;

namespace RenergeIA.Web.Services;

public class AlertaServiceSimple
{
    private readonly RenergeIADbContext _db;

    public AlertaServiceSimple(RenergeIADbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Genera alertas automáticas básicas para un proyecto
    /// </summary>
    public async Task<int> GenerarAlertasAutomaticasAsync(int proyectoId)
    {
        var alertasCreadas = 0;

        // 1. Actividades atrasadas > 10%
        var actividades = await _db.ActividadesWBS
            .Where(a => a.ProyectoId == proyectoId && a.Activo)
            .ToListAsync();

        foreach (var act in actividades)
        {
            var brecha = act.AvancePlanificado - act.AvanceReal;
            if (brecha > 10)
            {
                await CrearAlertaSiNoExisteAsync(proyectoId,
                    CategoriaAlerta.Avance,
                    "Actividad Atrasada",
                    $"'{act.Nombre}' con atraso de {brecha:F1}%",
                    brecha > 20 ? "Alta" : "Media");
                alertasCreadas++;
            }
        }

        // 2. Restricciones abiertas > 7 días
        var restricciones = await _db.Restricciones
            .Where(r => r.ProyectoId == proyectoId && r.Estado == EstadoRestriccion.Abierta)
            .ToListAsync();

        foreach (var r in restricciones)
        {
            var dias = (DateTime.Now - r.FechaIdentificacion).Days;
            if (dias > 7)
            {
                await CrearAlertaSiNoExisteAsync(proyectoId,
                    CategoriaAlerta.General,
                    "Restricción Sin Gestión",
                    $"Restricción '{r.Numero}' lleva {dias} días abierta",
                    dias > 15 ? "Alta" : "Media");
                alertasCreadas++;
            }
        }

        // 3. NC críticas abiertas
        var ncCriticas = await _db.NoConformidades
            .Where(nc => nc.ProyectoId == proyectoId &&
                        nc.Estado == EstadoNoConformidad.Abierta &&
                        nc.Severidad == SeveridadNoConformidad.Critica)
            .ToListAsync();

        foreach (var nc in ncCriticas)
        {
            await CrearAlertaSiNoExisteAsync(proyectoId,
                CategoriaAlerta.Seguridad,
                "NC Crítica Abierta",
                $"NC '{nc.Numero}': {nc.Titulo}",
                "Alta");
            alertasCreadas++;
        }

        await _db.SaveChangesAsync();
        return alertasCreadas;
    }

    private async Task CrearAlertaSiNoExisteAsync(int proyectoId, CategoriaAlerta categoria, string titulo, string mensaje, string severidad)
    {
        // Verificar si ya existe
        var existe = await _db.Alertas.AnyAsync(a =>
            a.ProyectoId == proyectoId &&
            a.Titulo == titulo &&
            a.Mensaje == mensaje &&
            !a.EsLeida);

        if (!existe)
        {
            _db.Alertas.Add(new Alerta
            {
                ProyectoId = proyectoId,
                Categoria = categoria,
                Titulo = titulo,
                Mensaje = mensaje,
                Severidad = severidad,
                EsLeida = false,
                FechaCreacion = DateTime.Now
            });
        }
    }

    /// <summary>
    /// Obtiene alertas activas de un proyecto
    /// </summary>
    public async Task<List<Alerta>> ObtenerAlertasActivasAsync(int proyectoId)
    {
        return await _db.Alertas
            .Where(a => a.ProyectoId == proyectoId && !a.EsLeida)
            .OrderByDescending(a => a.Severidad == "Alta" ? 3 : a.Severidad == "Media" ? 2 : 1)
            .ThenByDescending(a => a.FechaCreacion)
            .ToListAsync();
    }

    /// <summary>
    /// Marca alerta como leída
    /// </summary>
    public async Task MarcarComoLeidaAsync(int alertaId)
    {
        var alerta = await _db.Alertas.FindAsync(alertaId);
        if (alerta != null)
        {
            alerta.EsLeida = true;
            await _db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Resumen de alertas
    /// </summary>
    public async Task<ResumenAlertasSimple> ObtenerResumenAsync(int proyectoId)
    {
        var alertas = await ObtenerAlertasActivasAsync(proyectoId);

        return new ResumenAlertasSimple
        {
            Total = alertas.Count,
            Altas = alertas.Count(a => a.Severidad == "Alta"),
            Medias = alertas.Count(a => a.Severidad == "Media"),
            Bajas = alertas.Count(a => a.Severidad == "Baja")
        };
    }
}

public class ResumenAlertasSimple
{
    public int Total { get; set; }
    public int Altas { get; set; }
    public int Medias { get; set; }
    public int Bajas { get; set; }
}
