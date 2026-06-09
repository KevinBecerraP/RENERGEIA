using Microsoft.EntityFrameworkCore;
using RenergeIA.Core.Entities;
using RenergeIA.Core.Enums;
using RenergeIA.Infrastructure.Data;

namespace RenergeIA.Web.Services;

public class InformeDiarioService
{
    private readonly RenergeIADbContext _db;

    public InformeDiarioService(RenergeIADbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Calcula el avance esperado de una actividad en una fecha determinada
    /// según la programación inicial (FechaInicioPlanificada → FechaFinPlanificada)
    /// </summary>
    public decimal CalcularAvanceEsperado(ActividadWBS actividad, DateTime fecha)
    {
        if (fecha < actividad.FechaInicioPlanificada)
            return 0;

        if (fecha >= actividad.FechaFinPlanificada)
            return 100;

        var diasTotales = (actividad.FechaFinPlanificada - actividad.FechaInicioPlanificada).TotalDays;
        if (diasTotales <= 0)
            return 100;

        var diasTranscurridos = (fecha - actividad.FechaInicioPlanificada).TotalDays;
        var avance = (decimal)(diasTranscurridos / diasTotales * 100);

        return Math.Round(Math.Min(avance, 100), 2);
    }

    /// <summary>
    /// Calcula el avance acumulado de una actividad hasta una fecha determinada
    /// sumando todos los registros de avance previos
    /// </summary>
    public async Task<decimal> CalcularAvanceAcumuladoAsync(int actividadWBSId, DateTime hastaFecha)
    {
        var avanceTotal = await _db.RegistrosAvanceDiario
            .Where(r => r.ActividadWBSId == actividadWBSId && r.Fecha <= hastaFecha)
            .SumAsync(r => r.PorcentajeAvance);

        return Math.Round(Math.Min(avanceTotal, 100), 2);
    }

    /// <summary>
    /// Calcula la desviación entre el avance real y el esperado
    /// Negativo = atraso, Positivo = adelanto
    /// </summary>
    public decimal CalcularDesviacion(decimal avanceReal, decimal avanceEsperado)
    {
        return Math.Round(avanceReal - avanceEsperado, 2);
    }

    /// <summary>
    /// Calcula el SPI (Schedule Performance Index)
    /// SPI = Avance Real / Avance Esperado
    /// SPI < 1 = atraso, SPI = 1 = a tiempo, SPI > 1 = adelanto
    /// </summary>
    public decimal CalcularSPI(decimal avanceReal, decimal avanceEsperado)
    {
        if (avanceEsperado == 0)
            return 1;

        return Math.Round(avanceReal / avanceEsperado, 2);
    }

    /// <summary>
    /// Determina el estado de avance según la desviación
    /// </summary>
    public EstadoAvance DeterminarEstadoAvance(decimal desviacion)
    {
        return desviacion switch
        {
            < -5 => EstadoAvance.Atrasado,
            > 5 => EstadoAvance.Adelantado,
            _ => EstadoAvance.AlDia
        };
    }

    /// <summary>
    /// Calcula los días de atraso estimados según el ritmo de avance
    /// </summary>
    public int CalcularDiasAtraso(ActividadWBS actividad, decimal avanceReal, DateTime fechaActual)
    {
        if (avanceReal >= 100 || fechaActual < actividad.FechaInicioPlanificada)
            return 0;

        var avanceEsperado = CalcularAvanceEsperado(actividad, fechaActual);
        if (avanceReal >= avanceEsperado)
            return 0;

        var diasTotales = (actividad.FechaFinPlanificada - actividad.FechaInicioPlanificada).TotalDays;
        if (diasTotales <= 0)
            return 0;

        var diasEsperados = diasTotales * (double)(avanceEsperado / 100);
        var diasReales = diasTotales * (double)(avanceReal / 100);
        var atraso = diasEsperados - diasReales;

        return atraso > 0 ? (int)Math.Ceiling(atraso) : 0;
    }

    /// <summary>
    /// Actualiza automáticamente todos los campos calculados de un registro de avance
    /// </summary>
    public async Task ActualizarCalculosRegistroAvanceAsync(RegistroAvanceDiario registro)
    {
        var actividad = await _db.ActividadesWBS.FindAsync(registro.ActividadWBSId);
        if (actividad == null)
            return;

        // Calcular avance esperado
        registro.AvanceEsperado = CalcularAvanceEsperado(actividad, registro.Fecha);

        // Calcular avance acumulado
        registro.AvanceAcumulado = await CalcularAvanceAcumuladoAsync(registro.ActividadWBSId, registro.Fecha);

        // Calcular desviación
        registro.Desviacion = CalcularDesviacion(registro.AvanceAcumulado, registro.AvanceEsperado);

        // Determinar estado
        registro.Estado = DeterminarEstadoAvance(registro.Desviacion);

        // Calcular días de atraso
        registro.DiasAtraso = CalcularDiasAtraso(actividad, registro.AvanceAcumulado, registro.Fecha);
    }

    /// <summary>
    /// Obtiene el resumen de avance por disciplina para un proyecto en una fecha
    /// </summary>
    public async Task<List<ResumenDisciplina>> ObtenerResumenPorDisciplinaAsync(int proyectoId, DateTime fecha)
    {
        var actividades = await _db.ActividadesWBS
            .Where(a => a.ProyectoId == proyectoId && a.Activo)
            .ToListAsync();

        var resumen = new List<ResumenDisciplina>();

        foreach (var disciplina in Enum.GetValues<Disciplina>())
        {
            var actividadesDisciplina = actividades.Where(a => a.Disciplina == disciplina).ToList();
            if (!actividadesDisciplina.Any())
                continue;

            var avanceEsperadoPromedio = actividadesDisciplina.Average(a => CalcularAvanceEsperado(a, fecha));
            var avanceRealPromedio = actividadesDisciplina.Average(a => a.AvanceReal);

            resumen.Add(new ResumenDisciplina
            {
                Disciplina = disciplina,
                TotalActividades = actividadesDisciplina.Count,
                AvanceEsperado = Math.Round(avanceEsperadoPromedio, 2),
                AvanceReal = Math.Round(avanceRealPromedio, 2),
                Desviacion = Math.Round(avanceRealPromedio - avanceEsperadoPromedio, 2),
                SPI = CalcularSPI(Math.Round(avanceRealPromedio, 2), Math.Round(avanceEsperadoPromedio, 2))
            });
        }

        return resumen;
    }

    /// <summary>
    /// Genera datos para la Curva S del proyecto
    /// </summary>
    public async Task<List<PuntoCurvaS>> GenerarCurvaSAsync(int proyectoId, Disciplina? disciplina = null)
    {
        var actividades = await _db.ActividadesWBS
            .Where(a => a.ProyectoId == proyectoId && a.Activo)
            .ToListAsync();

        if (disciplina.HasValue)
            actividades = actividades.Where(a => a.Disciplina == disciplina.Value).ToList();

        if (!actividades.Any())
            return new List<PuntoCurvaS>();

        var fechaInicio = actividades.Min(a => a.FechaInicioPlanificada);
        var fechaFin = actividades.Max(a => a.FechaFinPlanificada);
        var puntos = new List<PuntoCurvaS>();

        for (var fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(7))
        {
            var avanceEsperado = actividades.Average(a => CalcularAvanceEsperado(a, fecha));
            var avanceReal = actividades.Average(a => a.AvanceReal);

            puntos.Add(new PuntoCurvaS
            {
                Fecha = fecha,
                AvanceEsperado = Math.Round(avanceEsperado, 2),
                AvanceReal = Math.Round(avanceReal, 2)
            });
        }

        return puntos;
    }

    /// <summary>
    /// Calcula la siguiente versión alfabética
    /// Ejemplos: "0.a" → "0.b", "0.z" → "0.aa", "0.c" → "0.d"
    /// </summary>
    public string SiguienteVersion(string versionActual)
    {
        if (string.IsNullOrEmpty(versionActual))
            return "0.a";

        var partes = versionActual.Split('.');
        if (partes.Length != 2)
            return "0.a";

        var prefijo = partes[0];
        var letra = partes[1];

        // Incrementar letra alfabéticamente
        var siguienteLetra = IncrementarLetra(letra);
        return $"{prefijo}.{siguienteLetra}";
    }

    /// <summary>
    /// Convierte una versión de desarrollo a versión aprobada
    /// Ejemplo: "0.c" → "1.0", "1.b" → "2.0"
    /// </summary>
    public string ConvertirAVersionAprobada(string versionActual)
    {
        if (string.IsNullOrEmpty(versionActual))
            return "1.0";

        var partes = versionActual.Split('.');
        if (partes.Length != 2 || !int.TryParse(partes[0], out var numero))
            return "1.0";

        return $"{numero + 1}.0";
    }

    /// <summary>
    /// Determina si una versión es aprobada (formato X.0)
    /// </summary>
    public bool EsVersionAprobada(string version)
    {
        if (string.IsNullOrEmpty(version))
            return false;

        return version.EndsWith(".0");
    }

    private string IncrementarLetra(string letra)
    {
        if (string.IsNullOrEmpty(letra))
            return "a";

        // Si termina en 'z', agregar otra 'a' (a → b → ... → z → aa)
        if (letra.All(c => c == 'z'))
            return new string('a', letra.Length + 1);

        var chars = letra.ToCharArray();
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            if (chars[i] < 'z')
            {
                chars[i]++;
                break;
            }
            chars[i] = 'a';
        }

        return new string(chars);
    }
}

public class ResumenDisciplina
{
    public Disciplina Disciplina { get; set; }
    public int TotalActividades { get; set; }
    public decimal AvanceEsperado { get; set; }
    public decimal AvanceReal { get; set; }
    public decimal Desviacion { get; set; }
    public decimal SPI { get; set; }
}

public class PuntoCurvaS
{
    public DateTime Fecha { get; set; }
    public decimal AvanceEsperado { get; set; }
    public decimal AvanceReal { get; set; }
}
