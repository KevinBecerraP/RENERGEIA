# 📘 RenergeIA - Guía de Implementación Detallada

## 📋 Índice

1. [Introducción](#introducción)
2. [Contexto del Proyecto](#contexto-del-proyecto)
3. [Configuración Inicial del Entorno](#configuración-inicial-del-entorno)
4. [Arquitectura del Proyecto](#arquitectura-del-proyecto)
5. [Proceso de Desarrollo Paso a Paso](#proceso-de-desarrollo-paso-a-paso)
6. [Modelo de Datos](#modelo-de-datos)
7. [Patrones y Buenas Prácticas](#patrones-y-buenas-prácticas)
8. [Proceso de Migración de Base de Datos](#proceso-de-migración-de-base-de-datos)
9. [Desarrollo de Módulos](#desarrollo-de-módulos)
10. [Integración de Librerías Externas](#integración-de-librerías-externas)
11. [Problemas Encontrados y Soluciones](#problemas-encontrados-y-soluciones)
12. [Próximos Pasos](#próximos-pasos)

---

## 🎯 Introducción

Este documento describe de manera **meticulosa y detallada** todo el proceso de implementación del proyecto **RenergeIA**, una plataforma web interna para la gestión de proyectos EPC (Engineering, Procurement, Construction) de plantas fotovoltaicas desarrollada para **Renergeia S.A.S.**

La guía cubre desde la configuración inicial del entorno de desarrollo hasta la implementación de cada módulo funcional, incluyendo todas las decisiones técnicas, problemas encontrados y soluciones aplicadas.

### 🎯 Objetivo del Documento

Proporcionar una guía completa que permita a cualquier desarrollador:
- Entender **cómo** se implementó cada característica
- Conocer **por qué** se tomaron ciertas decisiones técnicas
- Replicar el proceso de desarrollo
- Continuar con el desarrollo del proyecto
- Resolver problemas similares a los encontrados

---

## 🏢 Contexto del Proyecto

### Cliente
**Renergeia S.A.S.** - Empresa especializada en proyectos EPC de energía solar fotovoltaica con operaciones en:
- 🇨🇴 Colombia
- 🇵🇦 Panamá
- 🇪🇨 Ecuador
- 🇮🇹 Italia

### Problema a Resolver
El cliente gestionaba sus proyectos EPC mediante herramientas dispersas:
- **Microsoft Excel** para cronogramas y costos
- **Microsoft Project** para planificación
- **SharePoint** para documentos
- **WhatsApp** y correo electrónico para comunicación
- **Archivos físicos** para fotografías y registros

Esta dispersión generaba:
- ❌ Pérdida de información
- ❌ Dificultad para generar reportes consolidados
- ❌ Versiones desactualizadas de documentos
- ❌ Imposibilidad de tomar decisiones en tiempo real
- ❌ Alto tiempo invertido en consolidación manual de datos

### Solución Propuesta
Desarrollar **RenergeIA**, una plataforma web centralizada que integre:
- ✅ Gestión de proyectos
- ✅ Cronograma WBS
- ✅ Control de avance diario
- ✅ Gestión de personal y equipos
- ✅ Control de costos
- ✅ Repositorio de documentos con versionado
- ✅ Informe diario automatizado
- ✅ Dashboard ejecutivo con indicadores
- ✅ Histogramas de recursos
- ✅ Alertas en tiempo real
- ✅ Generación automática de reportes PDF

### Documentación de Referencia
- **Documento:** RenergeIA_Documentacion_v1.0.pdf
- **Páginas:** 33
- **Fecha:** Junio 2026
- **Contenido:** Requisitos funcionales, módulos, roles, reglas de negocio, alertas

---

## 💻 Configuración Inicial del Entorno

### Etapa 0: Preparación del Entorno de Desarrollo

#### 1. Sistema Operativo
```
OS: Windows 11 Pro
Versión: 10.0.26200
Usuario: Ing. Kevin (principiante en desarrollo)
```

**Nota importante:** El usuario es principiante, por lo que se optó por:
- Usar exclusivamente **Visual Studio Code** como IDE
- Evitar configuraciones complejas de terminal
- Proporcionar comandos claros y directos
- Documentar cada paso meticulosamente

#### 2. Instalación de Herramientas

##### 2.1 .NET SDK 10
```bash
# Verificar instalación
dotnet --version
# Salida: 10.0.300
```

**¿Por qué .NET 10?**
- ✅ Última versión estable de .NET
- ✅ Mejor rendimiento que versiones anteriores
- ✅ Soporte completo para Blazor Server
- ✅ Características modernas de C# 13

##### 2.2 SQL Server 2022 Developer Edition
```
Instalación: SQL Server Management Studio (SSMS)
Instancia: localhost
Autenticación: Windows Authentication
```

**Configuración de SQL Server:**
1. Instalación de SQL Server 2022
2. Instalación de SSMS
3. Creación de instancia local
4. Verificación de conectividad

##### 2.3 Visual Studio Code
**Extensiones instaladas:**
- C# Dev Kit
- C# Extensions
- .NET Runtime Install Tool
- Blazor Syntax Highlighter
- NuGet Package Manager

#### 3. Creación de la Estructura del Proyecto

##### 3.1 Arquitectura de 3 Capas

**Decisión técnica:** Se eligió arquitectura en capas por:
- ✅ Separación clara de responsabilidades
- ✅ Facilidad para testing
- ✅ Escalabilidad
- ✅ Mantenibilidad

**Estructura creada:**
```
PROYECTO AGENTE/
├── RenergeIA.Core/          # Capa de Dominio
├── RenergeIA.Infrastructure/ # Capa de Datos
└── RenergeIA.Web/           # Capa de Presentación
```

##### 3.2 Creación de Proyectos

**Comandos ejecutados:**
```bash
# Crear solución
dotnet new sln -n RenergeIA

# Crear proyecto Core (Class Library)
dotnet new classlib -n RenergeIA.Core -f net10.0

# Crear proyecto Infrastructure (Class Library)
dotnet new classlib -n RenergeIA.Infrastructure -f net10.0

# Crear proyecto Web (Blazor Server)
dotnet new blazor -n RenergeIA.Web -f net10.0 --interactivity Server

# Agregar proyectos a la solución
dotnet sln add RenergeIA.Core/RenergeIA.Core.csproj
dotnet sln add RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj
dotnet sln add RenergeIA.Web/RenergeIA.Web.csproj

# Agregar referencias entre proyectos
dotnet add RenergeIA.Infrastructure reference RenergeIA.Core
dotnet add RenergeIA.Web reference RenergeIA.Infrastructure
dotnet add RenergeIA.Web reference RenergeIA.Core
```

**¿Por qué esta estructura?**
- **RenergeIA.Core**: Contiene entidades, enums, interfaces (sin dependencias externas)
- **RenergeIA.Infrastructure**: Implementa acceso a datos con EF Core
- **RenergeIA.Web**: Capa de presentación con Blazor Server

#### 4. Instalación de Paquetes NuGet

##### 4.1 RenergeIA.Core
```bash
# Sin paquetes adicionales (solo .NET runtime)
```

##### 4.2 RenergeIA.Infrastructure
```bash
cd RenergeIA.Infrastructure

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore --version 10.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 10.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.0

# Identity para autenticación
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 10.0.0
```

**¿Por qué estos paquetes?**
- **EntityFrameworkCore**: ORM para mapeo objeto-relacional
- **SqlServer**: Proveedor para SQL Server
- **Tools**: CLI para migraciones
- **Design**: Soporte en tiempo de diseño
- **Identity**: Sistema de autenticación/autorización de ASP.NET Core

##### 4.3 RenergeIA.Web
```bash
cd RenergeIA.Web

# Ya incluye Blazor por defecto
# Paquetes adicionales agregados durante desarrollo según necesidad
```

#### 5. Configuración de Cadena de Conexión

**Archivo:** `RenergeIA.Web/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RenergeIADb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

**Parámetros importantes:**
- `Server=localhost`: Instancia local de SQL Server
- `Trusted_Connection=True`: Autenticación de Windows
- `TrustServerCertificate=True`: Necesario para desarrollo local
- `MultipleActiveResultSets=True`: Permite múltiples consultas simultáneas

---

## 🏗️ Arquitectura del Proyecto

### Arquitectura General

```
┌─────────────────────────────────────────────────────┐
│                   PRESENTACIÓN                       │
│         Blazor Server (RenergeIA.Web)               │
│  - Componentes .razor                                │
│  - Servicios de negocio                              │
│  - SignalR para tiempo real                          │
└─────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────┐
│              LÓGICA DE NEGOCIO                       │
│         Services (RenergeIA.Web/Services)            │
│  - InformeDiarioService                              │
│  - DocumentoService                                  │
│  - HistogramaService                                 │
└─────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────┐
│                  ACCESO A DATOS                      │
│     Entity Framework Core (Infrastructure)           │
│  - DbContext                                         │
│  - Migraciones                                       │
└─────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────┐
│                  BASE DE DATOS                       │
│            SQL Server 2022 (RenergeIADb)             │
│  - 30+ tablas                                        │
│  - Relaciones FK                                     │
│  - Índices                                           │
└─────────────────────────────────────────────────────┘
```

### Patrón de Diseño: Repository + Service

**No se implementó patrón Repository completo por:**
- ✅ Entity Framework Core ya abstrae el acceso a datos
- ✅ DbContext actúa como Unit of Work
- ✅ DbSet<T> actúa como Repository
- ✅ Evita sobre-ingeniería para un equipo pequeño

**Se implementó capa de Servicios por:**
- ✅ Lógica de negocio separada de la presentación
- ✅ Reutilización de código
- ✅ Facilita el testing
- ✅ Inyección de dependencias

### Flujo de una Operación CRUD

**Ejemplo: Crear un Proyecto**

```
1. Usuario → Formulario (CrearProyecto.razor)
             ↓
2. OnSubmit → Validación cliente
             ↓
3. DbContext.Proyectos.Add(proyecto)
             ↓
4. DbContext.SaveChangesAsync()
             ↓
5. SQL Server → INSERT INTO Proyectos (...)
             ↓
6. Navegación → Lista de proyectos
```

---

## 📊 Modelo de Datos

### Entidad Base (Patrón Auditoría)

**Todas las entidades heredan de `EntidadBase`:**

```csharp
public abstract class EntidadBase
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaModificacion { get; set; }
    public string? CreadoPor { get; set; }
    public string? ModificadoPor { get; set; }
}
```

**Ventajas:**
- ✅ Auditoría automática de todas las entidades
- ✅ DRY (Don't Repeat Yourself)
- ✅ Facilita consultas polimórficas

### Entidades Principales

#### 1. Proyecto
**Archivo:** `RenergeIA.Core/Entities/Proyecto.cs`

```csharp
public class Proyecto : EntidadBase
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public EstadoProyecto Estado { get; set; }
    public decimal CapacidadKWp { get; set; }
    public decimal PresupuestoContractual { get; set; }
    public DateTime FechaInicioPlanificada { get; set; }
    public DateTime FechaFinPlanificada { get; set; }
    
    // Navegación
    public ICollection<ActividadWBS> ActividadesWBS { get; set; }
    public ICollection<PersonalProyecto> PersonalProyecto { get; set; }
    public ICollection<Equipo> Equipos { get; set; }
    // ... más colecciones
}
```

**Decisiones de diseño:**
- `Codigo`: Índice único para búsquedas rápidas
- `CapacidadKWp`: decimal(10,2) para precisión
- `PresupuestoContractual`: decimal(18,2) para manejar grandes valores
- Colecciones lazy-loading deshabilitado (mejor rendimiento)

#### 2. ActividadWBS (Estructura Jerárquica)
**Archivo:** `RenergeIA.Core/Entities/ActividadWBS.cs`

```csharp
public class ActividadWBS : EntidadBase
{
    public string CodigoWBS { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int ProyectoId { get; set; }
    public int? ActividadPadreId { get; set; }
    public bool EsActiva { get; set; } = true;
    
    // Auto-referencia para jerarquía
    public ActividadWBS? ActividadPadre { get; set; }
    public ICollection<ActividadWBS> SubActividades { get; set; }
    
    // Navegación
    public Proyecto Proyecto { get; set; } = null!;
}
```

**Patrón auto-referencial:**
- Permite árbol de N niveles
- `ActividadPadreId` nullable para nodos raíz
- DeleteBehavior.Restrict para evitar eliminaciones en cascada accidentales

#### 3. InformeDiario
**Archivo:** `RenergeIA.Core/Entities/InformeDiario.cs`

```csharp
public class InformeDiario : EntidadBase
{
    public int ProyectoId { get; set; }
    public DateOnly Fecha { get; set; }
    public int NumeroInforme { get; set; }
    public EstadoInforme Estado { get; set; }
    public int? InformeDiarioAnteriorId { get; set; }
    
    // Versionado
    public InformeDiario? InformeDiarioAnterior { get; set; }
    
    // Relaciones
    public Proyecto Proyecto { get; set; } = null!;
    public ICollection<RegistroAvanceDiario> RegistrosAvance { get; set; }
    public ICollection<Fotografia> Fotografias { get; set; }
    public ICollection<Restriccion> Restricciones { get; set; }
}
```

**Sistema de versionado:**
- Cada informe puede referenciar al anterior
- Permite historial de modificaciones
- DeleteBehavior.NoAction para evitar ciclos

#### 4. PlantillaHistograma + ItemHistograma
**Archivos:** 
- `RenergeIA.Core/Entities/PlantillaHistograma.cs`
- `RenergeIA.Core/Entities/ItemHistograma.cs`

```csharp
public class PlantillaHistograma : EntidadBase
{
    public string Nombre { get; set; } = string.Empty;
    public decimal CapacidadMW { get; set; }
    public TipoHistograma Tipo { get; set; }
    public ICollection<ItemHistograma> Items { get; set; }
}

public class ItemHistograma : EntidadBase
{
    public int PlantillaHistogramaId { get; set; }
    public string Cargo { get; set; } = string.Empty;
    public decimal TiempoMeses { get; set; }
    
    // Distribución mensual (diseño desnormalizado intencional)
    public decimal Mes1 { get; set; }
    public decimal Mes2 { get; set; }
    // ... hasta Mes12
    
    public PlantillaHistograma PlantillaHistograma { get; set; } = null!;
}
```

**Decisión de diseño desnormalizado:**
- ❌ NO se usó tabla normalizada (MesId, Valor)
- ✅ 12 columnas separadas (Mes1-Mes12)

**Razones:**
- Consultas más simples y rápidas
- No hay joins adicionales
- Siempre son exactamente 12 meses
- Mapeo directo en gráficos

### Configuración de Entity Framework

**Archivo:** `RenergeIA.Infrastructure/Data/RenergeIADbContext.cs`

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Proyecto
    modelBuilder.Entity<Proyecto>(e =>
    {
        e.HasKey(p => p.Id);
        e.Property(p => p.Codigo).IsRequired().HasMaxLength(20);
        e.HasIndex(p => p.Codigo).IsUnique();
        e.Property(p => p.CapacidadKWp).HasColumnType("decimal(10,2)");
    });
    
    // ActividadWBS - auto-referencia
    modelBuilder.Entity<ActividadWBS>(e =>
    {
        e.HasOne(a => a.ActividadPadre)
         .WithMany(a => a.SubActividades)
         .HasForeignKey(a => a.ActividadPadreId)
         .OnDelete(DeleteBehavior.Restrict); // ¡Importante!
    });
    
    // RegistroAvanceDiario - múltiples FK a Proyecto
    modelBuilder.Entity<RegistroAvanceDiario>(e =>
    {
        e.HasOne(r => r.ActividadWBS)
         .WithMany(a => a.RegistrosAvance)
         .OnDelete(DeleteBehavior.Restrict); // Evita múltiples CASCADE
    });
}
```

**DeleteBehavior configurado cuidadosamente:**
- `Cascade`: Solo en relaciones padre-hijo directas
- `Restrict`: Cuando hay múltiples caminos hacia la misma tabla
- `NoAction`: Para relaciones de versionado

---

## 🔧 Proceso de Desarrollo Paso a Paso

### ETAPA 1: Configuración de Identity y Autenticación

#### Paso 1.1: Crear ApplicationUser

**Archivo:** `RenergeIA.Infrastructure/Identity/ApplicationUser.cs`

```csharp
public class ApplicationUser : IdentityUser
{
    public string? NombreCompleto { get; set; }
}
```

#### Paso 1.2: Configurar DbContext con Identity

```csharp
public class RenergeIADbContext : IdentityDbContext<ApplicationUser>
{
    public RenergeIADbContext(DbContextOptions<RenergeIADbContext> options) 
        : base(options) { }
    
    public DbSet<Proyecto> Proyectos => Set<Proyecto>();
    // ... más DbSets
}
```

#### Paso 1.3: Registrar servicios en Program.cs

```csharp
builder.Services.AddDbContext<RenergeIADbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<RenergeIADbContext>()
.AddDefaultTokenProviders();
```

#### Paso 1.4: Crear primera migración

```bash
dotnet ef migrations add Initial --project RenergeIA.Infrastructure --startup-project RenergeIA.Web

dotnet ef database update --project RenergeIA.Infrastructure --startup-project RenergeIA.Web
```

**Resultado:** Base de datos creada con tablas de Identity

#### Paso 1.5: Seed de usuarios y roles

**Archivo:** `RenergeIA.Infrastructure/Identity/DatabaseSeeder.cs`

```csharp
public static class DatabaseSeeder
{
    public static async Task SeedRolesAndAdminAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        // Crear roles
        string[] roles = { "Administrador", "Gerente", "Ingeniero" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
        
        // Crear usuario admin
        var adminEmail = "admin@renergeia.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                NombreCompleto = "Administrador Sistema",
                EmailConfirmed = true
            };
            
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Administrador");
        }
    }
}
```

**Llamada en Program.cs:**
```csharp
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await DatabaseSeeder.SeedRolesAndAdminAsync(roleManager, userManager);
}
```

### ETAPA 2: Módulo de Proyectos CRUD

#### Paso 2.1: Crear entidad Proyecto

Ver sección "Modelo de Datos" arriba.

#### Paso 2.2: Agregar DbSet al contexto

```csharp
public DbSet<Proyecto> Proyectos => Set<Proyecto>();
```

#### Paso 2.3: Crear migración

```bash
dotnet ef migrations add AgregarProyectos --project RenergeIA.Infrastructure --startup-project RenergeIA.Web
dotnet ef database update --project RenergeIA.Infrastructure --startup-project RenergeIA.Web
```

#### Paso 2.4: Crear página de listado

**Archivo:** `RenergeIA.Web/Components/Pages/Proyectos/Proyectos.razor`

```razor
@page "/proyectos"
@inject RenergeIADbContext Db
@using Microsoft.EntityFrameworkCore

<h3>Proyectos</h3>

<a href="/proyectos/crear" class="btn btn-primary">Nuevo Proyecto</a>

<table class="table">
    <thead>
        <tr>
            <th>Código</th>
            <th>Nombre</th>
            <th>Cliente</th>
            <th>Estado</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var proyecto in _proyectos)
        {
            <tr>
                <td>@proyecto.Codigo</td>
                <td>@proyecto.Nombre</td>
                <td>@proyecto.Cliente</td>
                <td>@proyecto.Estado</td>
            </tr>
        }
    </tbody>
</table>

@code {
    private List<Proyecto> _proyectos = new();
    
    protected override async Task OnInitializedAsync()
    {
        _proyectos = await Db.Proyectos.ToListAsync();
    }
}
```

#### Paso 2.5: Crear página de creación

**Archivo:** `RenergeIA.Web/Components/Pages/Proyectos/CrearProyecto.razor`

```razor
@page "/proyectos/crear"
@inject RenergeIADbContext Db
@inject NavigationManager Nav

<h3>Crear Proyecto</h3>

<EditForm Model="_proyecto" OnValidSubmit="GuardarAsync">
    <DataAnnotationsValidator />
    <ValidationSummary />
    
    <div class="mb-3">
        <label>Código:</label>
        <InputText @bind-Value="_proyecto.Codigo" class="form-control" />
    </div>
    
    <div class="mb-3">
        <label>Nombre:</label>
        <InputText @bind-Value="_proyecto.Nombre" class="form-control" />
    </div>
    
    <!-- más campos -->
    
    <button type="submit" class="btn btn-success">Guardar</button>
    <a href="/proyectos" class="btn btn-secondary">Cancelar</a>
</EditForm>

@code {
    private Proyecto _proyecto = new();
    
    private async Task GuardarAsync()
    {
        Db.Proyectos.Add(_proyecto);
        await Db.SaveChangesAsync();
        Nav.NavigateTo("/proyectos");
    }
}
```

### ETAPA 3: Módulo WBS con Árbol Jerárquico

#### Paso 3.1: Diseño del componente recursivo

**Desafío:** Mostrar árbol de actividades de N niveles

**Solución:** Componente recursivo que se llama a sí mismo

**Archivo:** `RenergeIA.Web/Components/Pages/WBS/ItemWBS.razor`

```razor
@if (Actividad is not null)
{
    <div class="wbs-item" style="margin-left: @((Nivel * 20))px">
        <div class="d-flex align-items-center">
            @if (Actividad.SubActividades.Any())
            {
                <button @onclick="() => _expandido = !_expandido" class="btn btn-sm">
                    @(_expandido ? "▼" : "▶")
                </button>
            }
            
            <span>@Actividad.CodigoWBS - @Actividad.Nombre</span>
            
            <label class="form-switch">
                <input type="checkbox" 
                       checked="@Actividad.EsActiva" 
                       @onchange="CambiarEstadoAsync" />
                Activa
            </label>
        </div>
        
        @if (_expandido && Actividad.SubActividades.Any())
        {
            @foreach (var sub in Actividad.SubActividades)
            {
                <!-- RECURSIÓN: componente se llama a sí mismo -->
                <ItemWBS Actividad="sub" 
                         Nivel="Nivel + 1" 
                         OnActualizarEstado="OnActualizarEstado" />
            }
        }
    </div>
}

@code {
    [Parameter] public ActividadWBS? Actividad { get; set; }
    [Parameter] public int Nivel { get; set; }
    [Parameter] public EventCallback OnActualizarEstado { get; set; }
    
    private bool _expandido;
    
    private async Task CambiarEstadoAsync()
    {
        await OnActualizarEstado.InvokeAsync();
    }
}
```

#### Paso 3.2: Cargar árbol completo con Include

**Desafío:** Entity Framework no carga subactividades automáticamente

**Solución:** Método recursivo con Include

```csharp
private async Task<List<ActividadWBS>> CargarArbolComplettoAsync(int proyectoId)
{
    // Cargar TODAS las actividades del proyecto
    var todasActividades = await Db.ActividadesWBS
        .Where(a => a.ProyectoId == proyectoId)
        .ToListAsync();
    
    // Construir árbol en memoria
    var raices = todasActividades.Where(a => a.ActividadPadreId == null).ToList();
    
    foreach (var raiz in raices)
    {
        CargarHijosRecursivo(raiz, todasActividades);
    }
    
    return raices;
}

private void CargarHijosRecursivo(ActividadWBS padre, List<ActividadWBS> todas)
{
    padre.SubActividades = todas
        .Where(a => a.ActividadPadreId == padre.Id)
        .ToList();
    
    foreach (var hijo in padre.SubActividades)
    {
        CargarHijosRecursivo(hijo, todas);
    }
}
```

**¿Por qué este enfoque?**
- ✅ Una sola consulta a la BD (N+1 problem evitado)
- ✅ Construcción del árbol en memoria
- ✅ Mejor rendimiento que Include anidados

#### Paso 3.3: Plantilla de 110 actividades

**Archivo:** `RenergeIA.Web/Data/PlantillaWBS.json` (creado manualmente)

```json
[
  {
    "codigo": "1",
    "nombre": "INGENIERÍA",
    "hijos": [
      {
        "codigo": "1.1",
        "nombre": "Ingeniería Básica",
        "hijos": [
          {
            "codigo": "1.1.1",
            "nombre": "Diseño eléctrico"
          },
          {
            "codigo": "1.1.2",
            "nombre": "Diseño civil"
          }
        ]
      }
    ]
  }
  // ... 110 actividades en total
]
```

**Carga de plantilla:**

```csharp
private async Task CargarPlantillaAsync(int proyectoId)
{
    var json = await File.ReadAllTextAsync("Data/PlantillaWBS.json");
    var plantilla = JsonSerializer.Deserialize<List<NodoPlantilla>>(json);
    
    foreach (var nodo in plantilla)
    {
        await CrearActividadRecursivaAsync(nodo, proyectoId, null);
    }
}

private async Task CrearActividadRecursivaAsync(
    NodoPlantilla nodo, 
    int proyectoId, 
    int? padreId)
{
    var actividad = new ActividadWBS
    {
        CodigoWBS = nodo.Codigo,
        Nombre = nodo.Nombre,
        ProyectoId = proyectoId,
        ActividadPadreId = padreId,
        EsActiva = true
    };
    
    Db.ActividadesWBS.Add(actividad);
    await Db.SaveChangesAsync();
    
    foreach (var hijo in nodo.Hijos ?? new())
    {
        await CrearActividadRecursivaAsync(hijo, proyectoId, actividad.Id);
    }
}
```

### ETAPA 4: Informe Diario con Carga Masiva

#### Paso 4.1: Diseño del servicio

**Archivo:** `RenergeIA.Web/Services/InformeDiarioService.cs`

```csharp
public class InformeDiarioService
{
    private readonly RenergeIADbContext _db;
    
    public async Task<InformeDiario> CrearInformeAsync(int proyectoId, DateOnly fecha)
    {
        // 1. Crear informe
        var informe = new InformeDiario
        {
            ProyectoId = proyectoId,
            Fecha = fecha,
            NumeroInforme = await ObtenerSiguienteNumeroAsync(proyectoId),
            Estado = EstadoInforme.Borrador
        };
        
        _db.InformesDiarios.Add(informe);
        await _db.SaveChangesAsync();
        
        // 2. Cargar actividades activas
        await CargarActividadesActivasAsync(informe.Id, proyectoId);
        
        return informe;
    }
    
    private async Task CargarActividadesActivasAsync(int informeId, int proyectoId)
    {
        var actividadesActivas = await _db.ActividadesWBS
            .Where(a => a.ProyectoId == proyectoId && a.EsActiva)
            .ToListAsync();
        
        var registros = actividadesActivas.Select(a => new RegistroAvanceDiario
        {
            InformeDiarioId = informeId,
            ActividadWBSId = a.Id,
            PorcentajeAvance = 0,
            // Calcular avance esperado según cronograma
            AvanceEsperado = CalcularAvanceEsperado(a)
        }).ToList();
        
        _db.RegistrosAvanceDiario.AddRange(registros);
        await _db.SaveChangesAsync();
    }
    
    private decimal CalcularAvanceEsperado(ActividadWBS actividad)
    {
        if (!actividad.FechaInicio.HasValue || !actividad.FechaFin.HasValue)
            return 0;
        
        var totalDias = (actividad.FechaFin.Value - actividad.FechaInicio.Value).Days;
        var diasTranscurridos = (DateTime.Today - actividad.FechaInicio.Value).Days;
        
        if (diasTranscurridos < 0) return 0;
        if (diasTranscurridos > totalDias) return 100;
        
        return Math.Round((decimal)diasTranscurridos / totalDias * 100, 2);
    }
}
```

**Registro del servicio:**
```csharp
builder.Services.AddScoped<InformeDiarioService>();
```

#### Paso 4.2: Tabla editable de actividades

**Desafío:** Tabla con 100+ filas editables eficientemente

**Solución:** Blazor con binding directo

```razor
<table class="table table-sm">
    <thead>
        <tr>
            <th>Actividad</th>
            <th>Avance Programado</th>
            <th>Avance Real</th>
            <th>Desviación</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var registro in _registros)
        {
            <tr>
                <td>@registro.ActividadWBS.Nombre</td>
                <td>@registro.AvanceEsperado %</td>
                <td>
                    <input type="number" 
                           @bind="registro.PorcentajeAvance"
                           @bind:event="oninput"
                           @onchange="() => CalcularDesviacion(registro)"
                           class="form-control form-control-sm"
                           min="0" max="100" step="0.1" />
                </td>
                <td class="@ClaseDesviacion(registro.Desviacion)">
                    @registro.Desviacion %
                </td>
            </tr>
        }
    </tbody>
</table>

@code {
    private void CalcularDesviacion(RegistroAvanceDiario registro)
    {
        registro.Desviacion = registro.PorcentajeAvance - registro.AvanceEsperado;
        registro.AvanceAcumulado = registro.PorcentajeAvance; // Simplificado
        StateHasChanged();
    }
    
    private string ClaseDesviacion(decimal desviacion)
    {
        if (desviacion < -5) return "text-danger fw-bold";
        if (desviacion > 5) return "text-success fw-bold";
        return "";
    }
}
```

### ETAPA 5: Dashboard Ejecutivo con Chart.js

#### Paso 5.1: Instalación de Chart.js

**Archivo:** `RenergeIA.Web/Components/App.razor`

```html
<head>
    <!-- otros scripts -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script>
    <script src="/js/chart-helper.js"></script>
</head>
```

#### Paso 5.2: Helper JavaScript

**Archivo:** `RenergeIA.Web/wwwroot/js/chart-helper.js`

```javascript
window.chartInstances = {};

window.destroyChart = function(canvasId) {
    if (window.chartInstances[canvasId]) {
        window.chartInstances[canvasId].destroy();
        delete window.chartInstances[canvasId];
    }
};

window.createCurvaSChart = function(canvasId, labels, dataProgramada, dataEjecutada) {
    window.destroyChart(canvasId);
    
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;
    
    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Programado',
                    data: dataProgramada,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    backgroundColor: 'rgba(75, 192, 192, 0.1)',
                    fill: true,
                    tension: 0.4
                },
                {
                    label: 'Ejecutado',
                    data: dataEjecutada,
                    borderColor: 'rgba(255, 99, 132, 1)',
                    backgroundColor: 'rgba(255, 99, 132, 0.1)',
                    fill: true,
                    tension: 0.4
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100,
                    ticks: {
                        callback: function(value) {
                            return value + '%';
                        }
                    }
                }
            }
        }
    });
};
```

#### Paso 5.3: Invocar desde Blazor

```razor
@inject IJSRuntime JS

<div style="height: 400px;">
    <canvas id="curvaSChart"></canvas>
</div>

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await RenderizarCurvaSAsync();
        }
    }
    
    private async Task RenderizarCurvaSAsync()
    {
        var labels = new[] { "Mes 1", "Mes 2", "Mes 3", "Mes 4", "Mes 5", "Mes 6" };
        var programado = new[] { 10, 25, 45, 65, 85, 100 };
        var ejecutado = new[] { 8, 22, 48, 63, 80, 95 };
        
        await JS.InvokeVoidAsync("createCurvaSChart", "curvaSChart", labels, programado, ejecutado);
    }
}
```

### ETAPA 6: Módulo Histogramas

#### Paso 6.1: Diseño del modelo de datos

**Decisión importante:** ¿Normalizar o desnormalizar?

**Opción A (Normalizada):**
```
PlantillaHistograma
ItemHistograma
  - CargoId
  - PlantillaId
ItemHistogramaMes
  - ItemHistogramaId
  - NumeroMes
  - Valor
```

**Opción B (Desnormalizada) - ELEGIDA:**
```
PlantillaHistograma
ItemHistograma
  - Mes1, Mes2, ..., Mes12
```

**Razones para elegir B:**
- ✅ Consultas más simples
- ✅ Sin joins adicionales
- ✅ Siempre son 12 meses exactos
- ✅ Mejor performance para gráficos
- ✅ Más fácil de mantener

#### Paso 6.2: Creación de entidades

Ver sección "Modelo de Datos"

#### Paso 6.3: Servicio de Histogramas

**Archivo:** `RenergeIA.Web/Services/HistogramaService.cs`

```csharp
public class HistogramaService
{
    public async Task<PlantillaHistograma?> ObtenerPlantillaPorCapacidadAsync(
        decimal capacidadMW, 
        TipoHistograma tipo)
    {
        // Buscar plantilla más cercana
        var plantillas = await _db.PlantillasHistogramas
            .Where(p => p.Tipo == tipo)
            .OrderBy(p => Math.Abs(p.CapacidadMW - capacidadMW))
            .ToListAsync();
        
        return plantillas.FirstOrDefault();
    }
    
    public async Task<HistogramaGraficoData> GenerarDatosGraficoAsync(int plantillaId)
    {
        var items = await ObtenerItemsPlantillaAsync(plantillaId);
        
        // Agrupar por cargo para apilar en el gráfico
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
            Labels = new[] { "Mes 1", "Mes 2", ..., "Mes 12" },
            Series = porCargo
        };
    }
}
```

#### Paso 6.4: Gráfico de área apilada

**JavaScript:**
```javascript
window.createStackedAreaChart = function(canvasId, labels, datasets) {
    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: datasets
        },
        options: {
            scales: {
                x: { stacked: true },
                y: { stacked: true, beginAtZero: true }
            },
            elements: {
                line: { tension: 0.4 },
                point: { radius: 0 }
            }
        }
    });
};
```

**Blazor:**
```csharp
private async Task RenderizarGraficoAsync()
{
    var datosGrafico = await HistogramaService.GenerarDatosGraficoAsync(_plantillaId);
    
    var datasets = datosGrafico.Series.Select((s, idx) => new
    {
        label = s.Nombre,
        data = s.Valores.Select(v => (double)v).ToArray(),
        backgroundColor = ObtenerColor(idx, 0.6),
        borderColor = ObtenerColor(idx, 1),
        fill = true
    }).ToArray();
    
    await JS.InvokeVoidAsync("createStackedAreaChart", "histogramaChart", 
        datosGrafico.Labels, datasets);
}
```

#### Paso 6.5: Diseño moderno y colorido

**Paleta de colores vibrantes:**
```csharp
private string ObtenerColor(int index, double opacity)
{
    var colores = new[]
    {
        $"rgba(138, 43, 226, {opacity})",   // Violeta eléctrico
        $"rgba(255, 20, 147, {opacity})",   // Rosa neón
        $"rgba(0, 255, 127, {opacity})",    // Verde primavera
        $"rgba(255, 140, 0, {opacity})",    // Naranja oscuro
        // ... más colores
    };
    
    return colores[index % colores.Length];
}
```

**Gradientes dinámicos:**
```razor
<div class="card-header" style="background: linear-gradient(135deg, 
    @(_tipoSeleccionado == TipoHistograma.Personal 
        ? "#667eea 0%, #764ba2 100%" 
        : "#f093fb 0%, #f5576c 100%"));">
    <h5 class="text-white">Distribución Mensual</h5>
</div>
```

---

## 🗄️ Proceso de Migración de Base de Datos

### Comandos Esenciales

#### Crear nueva migración
```bash
dotnet ef migrations add NombreMigracion \
  --project RenergeIA.Infrastructure \
  --startup-project RenergeIA.Web
```

#### Aplicar migración
```bash
dotnet ef database update \
  --project RenergeIA.Infrastructure \
  --startup-project RenergeIA.Web
```

#### Revertir última migración
```bash
dotnet ef migrations remove \
  --project RenergeIA.Infrastructure \
  --startup-project RenergeIA.Web
```

#### Ver migraciones pendientes
```bash
dotnet ef migrations list \
  --project RenergeIA.Infrastructure \
  --startup-project RenergeIA.Web
```

### Anatomía de una Migración

**Archivo generado:** `YYYYMMDDHHMMSS_NombreMigracion.cs`

```csharp
public partial class AgregarHistogramas : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PlantillasHistogramas",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(maxLength: 100, nullable: false),
                CapacidadMW = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                Tipo = table.Column<int>(nullable: false),
                FechaCreacion = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlantillasHistogramas", x => x.Id);
            });
        
        migrationBuilder.CreateTable(
            name: "ItemsHistograma",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PlantillaHistogramaId = table.Column<int>(nullable: false),
                Cargo = table.Column<string>(maxLength: 200, nullable: false),
                Mes1 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                // ... Mes2 a Mes12
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ItemsHistograma", x => x.Id);
                table.ForeignKey(
                    name: "FK_ItemsHistograma_PlantillasHistogramas",
                    column: x => x.PlantillaHistogramaId,
                    principalTable: "PlantillasHistogramas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ItemsHistograma");
        migrationBuilder.DropTable(name: "PlantillasHistogramas");
    }
}
```

### Buenas Prácticas en Migraciones

1. **Nombrar descriptivamente:**
   - ✅ `AgregarTablasHistogramas`
   - ❌ `Migration001`

2. **Migración atómica:**
   - Una migración = un cambio lógico
   - No mezclar módulos diferentes

3. **Verificar antes de aplicar:**
   ```bash
   # Ver SQL que se ejecutará
   dotnet ef migrations script
   ```

4. **Backup antes de producción:**
   ```sql
   BACKUP DATABASE RenergeIADb TO DISK = 'backup.bak'
   ```

5. **Rollback plan:**
   - Siempre tener el método `Down()` implementado
   - Probar rollback en desarrollo

---

## 📦 Integración de Librerías Externas

### Chart.js 4.4.0

**Instalación:**
```html
<!-- App.razor -->
<script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script>
```

**Ventajas:**
- ✅ Sin instalación de npm
- ✅ CDN rápido
- ✅ Compatible con Blazor Server

**Integración con Blazor:**
- JavaScript Interop (`IJSRuntime`)
- Funciones helper en `/wwwroot/js/chart-helper.js`
- Gestión de instancias para evitar memory leaks

### Bootstrap Icons

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css">
```

**Uso:**
```html
<i class="bi bi-graph-up-arrow"></i>
<i class="bi bi-people-fill"></i>
```

### Entity Framework Core 10.0

**Paquetes:**
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

**Características usadas:**
- DbContext
- Migrations
- Lazy Loading deshabilitado (mejor performance)
- Include para carga eager
- AsNoTracking para consultas read-only

---

## 🐛 Problemas Encontrados y Soluciones

### Problema 1: Múltiples CASCADE DELETE

**Error:**
```
Introducing FOREIGN KEY constraint may cause cycles or multiple cascade paths.
```

**Causa:**
`RegistroAvanceDiario` tiene FK a:
- `ActividadWBS` → que tiene FK a `Proyecto`
- `InformeDiario` → que tiene FK a `Proyecto`

Dos caminos hacia `Proyecto` = SQL Server rechaza múltiples CASCADE

**Solución:**
```csharp
modelBuilder.Entity<RegistroAvanceDiario>(e =>
{
    e.HasOne(r => r.ActividadWBS)
     .WithMany(a => a.RegistrosAvance)
     .OnDelete(DeleteBehavior.Restrict); // Cambiar de Cascade a Restrict
});
```

### Problema 2: Carpeta con espacios en nombre

**Error:**
```bash
# No funciona:
cd c:\Users\Ing. Kevin\Desktop\PROYECTO AGENTE
```

**Solución:**
```bash
# Usar comillas:
cd "c:\Users\Ing. Kevin\Desktop\PROYECTO AGENTE"
```

### Problema 3: Hot Reload no funciona en archivos .razor

**Causa:**
Blazor Server a veces no detecta cambios en archivos .razor

**Soluciones probadas:**

1. **Forzar recarga del navegador:**
   ```
   Ctrl + Shift + R (sin caché)
   ```

2. **Reiniciar servidor:**
   ```bash
   Ctrl + C
   dotnet run
   ```

3. **Limpiar y recompilar:**
   ```bash
   dotnet clean
   dotnet build
   ```

### Problema 4: Chart.js no renderiza al primer render

**Causa:**
Canvas no está disponible en `OnInitialized`, solo después de render

**Solución:**
```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        await Task.Delay(100); // Dar tiempo al DOM
        await RenderizarGraficoAsync();
    }
}
```

### Problema 5: Árbol WBS carga lento (N+1 problem)

**Problema inicial:**
```csharp
// Carga recursiva = N consultas
foreach (var actividad in actividades)
{
    actividad.SubActividades = await Db.ActividadesWBS
        .Where(a => a.ActividadPadreId == actividad.Id)
        .ToListAsync();
}
```

**Solución:**
```csharp
// Una sola consulta
var todas = await Db.ActividadesWBS
    .Where(a => a.ProyectoId == proyectoId)
    .ToListAsync();

// Construir árbol en memoria
var raices = todas.Where(a => a.ActividadPadreId == null);
foreach (var raiz in raices)
{
    CargarHijosRecursivo(raiz, todas);
}
```

### Problema 6: Servidor corriendo bloquea compilación

**Error:**
```
MSB3061: No se puede eliminar el archivo RenergeIA.Web.dll. 
Access to the path denied. El archivo se ha bloqueado por: "RenergeIA.Web (22172)"
```

**Solución:**
1. Detener servidor: `Ctrl + C`
2. Compilar: `dotnet build`
3. Ejecutar: `dotnet run`

---

## 🎨 Patrones y Buenas Prácticas

### Patrón Service Layer

**Estructura:**
```
Controllers/Pages → Services → DbContext → Database
```

**Ejemplo:**
```csharp
// ❌ MAL: Lógica en página
@code {
    private async Task GuardarAsync()
    {
        var actividades = await Db.ActividadesWBS.Where(...).ToListAsync();
        foreach (var act in actividades)
        {
            var registro = new RegistroAvanceDiario { ... };
            Db.Add(registro);
        }
        await Db.SaveChangesAsync();
    }
}

// ✅ BIEN: Lógica en servicio
@inject InformeDiarioService Service

@code {
    private async Task GuardarAsync()
    {
        await Service.CrearInformeConRegistrosAsync(proyectoId, fecha);
    }
}
```

### Inyección de Dependencias

**Registro en Program.cs:**
```csharp
builder.Services.AddDbContext<RenergeIADbContext>(options => ...);
builder.Services.AddScoped<InformeDiarioService>();
builder.Services.AddScoped<DocumentoService>();
builder.Services.AddScoped<HistogramaService>();
```

**Uso en componentes:**
```razor
@inject RenergeIADbContext Db
@inject InformeDiarioService Service
@inject NavigationManager Nav
@inject IJSRuntime JS
```

### Nomenclatura

**Entidades:**
- PascalCase
- Singular
- Sufijo descriptivo si necesario

**DbSets:**
- PascalCase
- Plural
- `public DbSet<Proyecto> Proyectos => Set<Proyecto>();`

**Servicios:**
- PascalCase
- Sufijo `Service`
- `InformeDiarioService`, `HistogramaService`

**Migraciones:**
- PascalCase descriptivo
- Prefijo con verbo
- `AgregarTablasHistogramas`, `ModificarCampoProyecto`

### Async/Await

**Siempre usar async para I/O:**
```csharp
// ✅ BIEN
public async Task<List<Proyecto>> ObtenerProyectosAsync()
{
    return await Db.Proyectos.ToListAsync();
}

// ❌ MAL
public List<Proyecto> ObtenerProyectos()
{
    return Db.Proyectos.ToList(); // Bloquea el thread
}
```

### Validación

**Client-side con DataAnnotations:**
```csharp
public class Proyecto
{
    [Required(ErrorMessage = "El código es obligatorio")]
    [MaxLength(20)]
    public string Codigo { get; set; }
    
    [Range(1, 1000000, ErrorMessage = "Capacidad entre 1 y 1,000,000 kWp")]
    public decimal CapacidadKWp { get; set; }
}
```

**En componentes:**
```razor
<EditForm Model="_proyecto" OnValidSubmit="GuardarAsync">
    <DataAnnotationsValidator />
    <ValidationSummary />
    
    <InputText @bind-Value="_proyecto.Codigo" />
    <ValidationMessage For="() => _proyecto.Codigo" />
</EditForm>
```

---

## 🚀 Próximos Pasos

### Módulos Pendientes (Fase 1)

1. **Módulo de Costos**
   - CRUD de partidas presupuestarias
   - Registro de costos reales
   - Comparativa presupuesto vs real
   - Dashboard de costos por disciplina

2. **Módulo de No Conformidades**
   - Registro de NC con fotografías
   - Acciones correctivas
   - Seguimiento hasta cierre
   - Reportes por categoría

3. **Módulo de Restricciones**
   - CRUD de restricciones
   - Vinculación con actividades
   - Estados (Abierta, En gestión, Resuelta)
   - Dashboard de restricciones críticas

### Mejoras Técnicas Planificadas

1. **Performance:**
   - Implementar caché con `IMemoryCache`
   - Paginación en listados grandes
   - Lazy loading para imágenes

2. **UX/UI:**
   - Mejorar diseño de formularios
   - Añadir animaciones suaves
   - Modo oscuro/claro

3. **Reportes:**
   - Integración con QuestPDF
   - Generación automática de informes
   - Exportación a Excel

4. **Integración:**
   - API REST para integraciones
   - Webhooks para notificaciones
   - Conexión con SharePoint

### Fase 2 (Meses 5-8)

- Dashboard gerencial avanzado
- Integración con IA (OpenAI API)
- Reportes automáticos programados
- Integración con clima API
- WhatsApp Business API

### Fase 3 (Meses 9-14)

- Módulo HSE completo
- Gestión de compras y proveedores
- QA/QC avanzado
- Predicción de avance por históricos
- Machine Learning para alertas

---

## 📝 Resumen de Tecnologías Utilizadas

| Categoría | Tecnología | Versión | Propósito |
|-----------|-----------|---------|-----------|
| **Backend** | .NET | 10.0 | Framework principal |
| | C# | 13 | Lenguaje de programación |
| | ASP.NET Core | 10.0 | Web framework |
| | Entity Framework Core | 10.0 | ORM |
| **Frontend** | Blazor Server | 10.0 | UI interactiva |
| | Bootstrap | 5.3 | CSS framework |
| | Chart.js | 4.4.0 | Gráficos |
| | Bootstrap Icons | 1.11 | Iconografía |
| **Base de Datos** | SQL Server | 2022 | RDBMS |
| | SQL Server Management Studio | 20.x | Administración |
| **Autenticación** | ASP.NET Core Identity | 10.0 | Auth/Authz |
| **Herramientas** | Visual Studio Code | Latest | IDE |
| | Git | 2.x | Control de versiones |
| | GitHub | - | Repositorio remoto |
| **Sistema Operativo** | Windows | 11 Pro | Desarrollo |

---

## 📚 Referencias y Recursos

### Documentación Oficial
- [.NET 10 Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Chart.js Documentation](https://www.chartjs.org/docs/)

### Repositorio del Proyecto
- **GitHub:** https://github.com/KevinBecerraP/RENERGEIA.git
- **Branch principal:** `master`

### Contacto del Proyecto
- **Cliente:** Renergeia S.A.S.
- **Desarrollador:** Ing. Kevin
- **Email:** luisabecerra22@gmail.com

---

## 🎯 Conclusión

Este documento ha cubierto exhaustivamente el proceso de implementación de **RenergeIA**, desde la configuración inicial del entorno hasta el desarrollo completo de 8 módulos funcionales.

El proyecto demuestra:
- ✅ Arquitectura en capas bien definida
- ✅ Uso efectivo de Entity Framework Core
- ✅ Componentes Blazor reutilizables
- ✅ Integración con librerías JavaScript (Chart.js)
- ✅ Diseño moderno y responsivo
- ✅ Buenas prácticas de desarrollo

Con esta base sólida, el proyecto está preparado para continuar su desarrollo hacia las fases 2 y 3, incorporando características avanzadas como IA, integraciones externas y módulos especializados.

---

**Última actualización:** Junio 14, 2026  
**Versión del documento:** 1.0  
**Etapa actual:** Etapa 12 - Módulo Histogramas completado
