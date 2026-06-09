using Microsoft.EntityFrameworkCore;
using RenergeIA.Core.Entities;
using RenergeIA.Core.Enums;
using RenergeIA.Infrastructure.Data;

namespace RenergeIA.Web.Services;

public class DocumentoService
{
    private readonly RenergeIADbContext _db;
    private readonly IWebHostEnvironment _env;
    private const long MaxFileSize = 50 * 1024 * 1024; // 50 MB

    public DocumentoService(RenergeIADbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    /// <summary>
    /// Guarda un archivo en el sistema y retorna la ruta relativa
    /// </summary>
    public async Task<(bool Success, string? RutaArchivo, string? Error)> GuardarArchivoAsync(
        int proyectoId,
        int documentoId,
        string version,
        Stream fileStream,
        string nombreArchivo)
    {
        try
        {
            // Validar tamaño
            if (fileStream.Length > MaxFileSize)
            {
                return (false, null, $"El archivo excede el tamaño máximo permitido de {MaxFileSize / 1024 / 1024} MB");
            }

            // Validar extensión
            var extension = Path.GetExtension(nombreArchivo).ToLowerInvariant();
            var extensionesPermitidas = new[] { ".pdf", ".dwg", ".xlsx", ".xls", ".docx", ".doc", ".png", ".jpg", ".jpeg", ".zip", ".rar" };

            if (!extensionesPermitidas.Contains(extension))
            {
                return (false, null, $"Extensión {extension} no permitida. Extensiones válidas: {string.Join(", ", extensionesPermitidas)}");
            }

            // Crear directorio si no existe
            var carpetaProyecto = Path.Combine(_env.WebRootPath, "uploads", "documentos", proyectoId.ToString(), documentoId.ToString());
            Directory.CreateDirectory(carpetaProyecto);

            // Nombre único del archivo
            var nombreUnico = $"{documentoId}_v{version}_{nombreArchivo}";
            var rutaCompleta = Path.Combine(carpetaProyecto, nombreUnico);

            // Guardar archivo
            using (var fileStreamDestino = new FileStream(rutaCompleta, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamDestino);
            }

            // Ruta relativa para almacenar en BD
            var rutaRelativa = Path.Combine("uploads", "documentos", proyectoId.ToString(), documentoId.ToString(), nombreUnico);

            return (true, rutaRelativa, null);
        }
        catch (Exception ex)
        {
            return (false, null, $"Error al guardar el archivo: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene la ruta completa de un archivo
    /// </summary>
    public string ObtenerRutaCompleta(string rutaRelativa)
    {
        return Path.Combine(_env.WebRootPath, rutaRelativa);
    }

    /// <summary>
    /// Genera el siguiente número de versión para un documento
    /// Formato: Rev 0, Rev A, Rev B, ..., Rev Z, Rev AA, etc.
    /// </summary>
    public async Task<string> SiguienteVersionAsync(int documentoId)
    {
        var versiones = await _db.VersionesDocumento
            .Where(v => v.DocumentoId == documentoId)
            .OrderByDescending(v => v.FechaSubida)
            .Select(v => v.NumeroVersion)
            .ToListAsync();

        if (!versiones.Any())
        {
            return "Rev 0";
        }

        var ultimaVersion = versiones.First();

        // Si es Rev 0, siguiente es Rev A
        if (ultimaVersion == "Rev 0")
        {
            return "Rev A";
        }

        // Extraer la letra (Rev A -> A)
        var letra = ultimaVersion.Replace("Rev ", "").Trim();

        // Incrementar letra
        var siguienteLetra = IncrementarLetra(letra);

        return $"Rev {siguienteLetra}";
    }

    /// <summary>
    /// Incrementa una letra: A -> B, Z -> AA, AZ -> BA
    /// </summary>
    private string IncrementarLetra(string letra)
    {
        if (string.IsNullOrEmpty(letra))
        {
            return "A";
        }

        var chars = letra.ToCharArray();
        var carry = true;

        for (int i = chars.Length - 1; i >= 0 && carry; i--)
        {
            if (chars[i] == 'Z')
            {
                chars[i] = 'A';
            }
            else
            {
                chars[i]++;
                carry = false;
            }
        }

        if (carry)
        {
            return "A" + new string(chars);
        }

        return new string(chars);
    }

    /// <summary>
    /// Marca todas las versiones de un documento como NO actuales
    /// </summary>
    public async Task DesmarcarVersionesActualesAsync(int documentoId)
    {
        var versiones = await _db.VersionesDocumento
            .Where(v => v.DocumentoId == documentoId)
            .ToListAsync();

        foreach (var version in versiones)
        {
            version.EsVersionActual = false;
        }

        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Obtiene el tamaño formateado de un archivo
    /// </summary>
    public string FormatearTamanio(long bytes)
    {
        string[] unidades = { "B", "KB", "MB", "GB" };
        double size = bytes;
        int unidad = 0;

        while (size >= 1024 && unidad < unidades.Length - 1)
        {
            size /= 1024;
            unidad++;
        }

        return $"{size:F2} {unidades[unidad]}";
    }

    /// <summary>
    /// Valida que el código del documento sea único en el proyecto
    /// </summary>
    public async Task<bool> CodigoEsUnicoAsync(int proyectoId, string codigo, int? documentoIdExcluir = null)
    {
        var existe = await _db.Documentos
            .Where(d => d.ProyectoId == proyectoId && d.Codigo == codigo)
            .Where(d => !documentoIdExcluir.HasValue || d.Id != documentoIdExcluir.Value)
            .AnyAsync();

        return !existe;
    }

    /// <summary>
    /// Elimina un archivo físico del sistema
    /// </summary>
    public bool EliminarArchivo(string rutaRelativa)
    {
        try
        {
            var rutaCompleta = ObtenerRutaCompleta(rutaRelativa);

            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
