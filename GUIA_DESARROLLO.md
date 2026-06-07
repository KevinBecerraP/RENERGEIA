# RenergeIA — Guía de Desarrollo Paso a Paso

Registro completo de todo lo construido: qué se hizo, por qué, en qué orden y cómo.  
Actualizar este archivo cada vez que se avance en una nueva etapa o paso.

---

## ¿Cómo obtener el proyecto desde GitHub?

Si alguien necesita trabajar en este proyecto en un computador nuevo (o el equipo crece y entra otra persona), no es necesario empezar desde cero. Todo el código está en GitHub y se puede descargar en minutos.

### Requisitos previos en el nuevo equipo

Antes de clonar, instalar estas herramientas:

| Herramienta | Para qué |
|---|---|
| **Git** — [git-scm.com](https://git-scm.com) | Descargar y versionar el código |
| **.NET SDK 10** — [dotnet.microsoft.com](https://dotnet.microsoft.com/download) | Compilar y correr la app |
| **SQL Server 2025 Developer** — [microsoft.com/sql-server](https://www.microsoft.com/es-co/sql-server/sql-server-downloads) | Base de datos local |
| **VS Code** — [code.visualstudio.com](https://code.visualstudio.com) | Editor de código |
| **Extensión C# Dev Kit** en VS Code | IntelliSense y errores en tiempo real |

### Paso 1 — Clonar el repositorio

Abrir una terminal (PowerShell o CMD) y ejecutar:

```powershell
git clone https://github.com/KevinBecerraP/RENERGEIA.git
cd RENERGEIA
```

Esto descarga todo el código fuente en una carpeta llamada `RENERGEIA`.

### Paso 2 — Crear la base de datos

El proyecto **no incluye la base de datos** (está en `.gitignore`). Hay que crearla en el nuevo equipo:

```powershell
dotnet ef database update --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj
```

Este comando lee las migraciones del repositorio y crea todas las tablas automáticamente en SQL Server local.

> Si SQL Server no está en `localhost` (instancia por defecto), ajustar el archivo `RenergeIA.Web/appsettings.json`:
> ```json
> "DefaultConnection": "Server=TU_SERVIDOR;Database=RenergeIA;Trusted_Connection=True;TrustServerCertificate=True;"
> ```

### Paso 3 — Correr la aplicación

```powershell
dotnet run --project RenergeIA.Web/RenergeIA.Web.csproj
```

Abrir en el navegador: `http://localhost:5288`

### Paso 4 — Ingresar con el usuario administrador

Al arrancar por primera vez, el sistema crea automáticamente el usuario admin:

| Campo | Valor |
|---|---|
| Correo | `admin@renergeia.com` |
| Contraseña | `Admin123!` |

> Este usuario lo crea `DatabaseSeeder.cs` automáticamente si no existe. No hay que insertar nada manualmente en la base de datos.

### Resumen en 4 comandos

```powershell
git clone https://github.com/KevinBecerraP/RENERGEIA.git
cd RENERGEIA
dotnet ef database update --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj
dotnet run --project RenergeIA.Web/RenergeIA.Web.csproj
```

---

## ETAPA 0 — Decisiones previas y contexto del proyecto

### ¿Qué es RenergeIA?
Plataforma web interna para gestión de proyectos EPC (Engineering, Procurement, Construction) fotovoltaicos de Renergeia S.A.S. (Colombia, Panamá, Ecuador, Italia).

**Problema que resuelve:** el flujo de trabajo estaba disperso entre Excel, SharePoint, WhatsApp, correos y MS Project. RenergeIA centraliza todo.

### Tecnologías elegidas y por qué

| Tecnología | Rol | Por qué |
|---|---|---|
| Blazor Server | Capa de presentación (UI) | Web, sin Angular/React, C# puro en servidor |
| ASP.NET Core | Backend framework | Base de Blazor Server, robusto y gratuito |
| Entity Framework Core | ORM (acceso a datos) | Crea tablas desde clases C#, sin SQL manual |
| ASP.NET Core Identity | Autenticación/Autorización | Sistema de login + roles integrado con EF Core |
| SQL Server | Base de datos | Relacional, compatible con EF Core, herramienta visual SSMS |
| Bootstrap | Estilos CSS | Ya incluido en la plantilla Blazor, sin configuración extra |
| SignalR | Alertas en tiempo real | Ya integrado en Blazor Server |
| QuestPDF | Generación de PDFs | Librería C# moderna para informes |
| Git | Control de versiones | Historial, ramas, colaboración futura en GitHub |

### Arquitectura en 3 capas

```
RenergeIA.Web           → Blazor Server (páginas .razor, componentes)
RenergeIA.Core          → Entidades de dominio, interfaces (sin dependencias externas)
RenergeIA.Infrastructure → EF Core, repositorios, acceso a base de datos
```

**Regla de dependencias:**
- `Web` conoce a `Core` e `Infrastructure`
- `Infrastructure` conoce a `Core`
- `Core` no conoce a nadie (es la base)

### Fases del proyecto

| Fase | Meses | Contenido |
|---|---|---|
| Fase 1 | 1–4 | Login, proyectos, WBS, avance diario, costos, documentos, fotos, restricciones, informe diario, alertas |
| Fase 2 | 5–8 | Dashboard gerencial, IA con OpenAI API, reportes automáticos, SharePoint, WhatsApp API, clima |
| Fase 3 | 9–14 | HSE completo, compras, QA/QC avanzado, predicción por históricos |

### Números clave del sistema
- 18 módulos funcionales
- 13 roles RBAC (control de acceso por rol)
- 15 reglas de negocio (RN-01 a RN-15)
- 20 alertas automáticas (A-01 a A-20)
- Entidad central: `Proyecto`

---

## ETAPA 1 — Preparación del entorno

### 1.1 Herramientas instaladas

Instalar en este orden:

1. **Visual Studio Code** — editor de código
2. **Git** — control de versiones
3. **.NET SDK 10** — plataforma de desarrollo
4. **SQL Server 2025 Developer Edition** — base de datos
5. **SSMS (SQL Server Management Studio)** — interfaz visual para SQL Server
6. **Extensiones de VS Code** (ver sección 1.2)

### 1.2 Extensiones de VS Code requeridas

Instalar desde el panel de extensiones (`Ctrl + Shift + X`):

| Extensión | ID | Para qué sirve |
|---|---|---|
| C# Dev Kit | `ms-dotnettools.csdevkit` | IntelliSense, errores en tiempo real para C# |
| SQL Server (mssql) | `ms-mssql.mssql` | Conectar y ejecutar queries a SQL Server desde VS Code |

### 1.3 Versiones instaladas (entorno real del proyecto)

| Herramienta | Versión |
|---|---|
| .NET SDK | 10.0.300 |
| SQL Server | 2025 Developer Edition |
| Git | 2.54.0.windows.1 |
| Sistema operativo | Windows 11 |

### 1.4 Configurar Git (solo se hace una vez)

Abrir terminal y ejecutar:

```powershell
git config --global user.name "Kevin Becerra"
git config --global user.email "kevinbecerram07@hotmail.com"
```

Verificar:
```powershell
git config --list
```

### 1.5 Conectar SSMS a SQL Server

**IMPORTANTE:** La edición instalada es **Developer Edition** (instancia por defecto: `MSSQLSERVER`), NO la Express Edition. Por esto, en SSMS usar:

- **Server name:** `localhost` (NO escribir `localhost\SQLEXPRESS`)
- **Authentication:** Windows Authentication
- **Trust server certificate:** marcado

Si aparece el árbol con `Databases`, `Security`, `Server Objects` → la conexión está correcta.

**Cadena de conexión para el proyecto:**
```
Server=localhost;Database=RenergeIA;Trusted_Connection=True;TrustServerCertificate=True;
```

### 1.6 Crear la solución y estructura de proyectos

Abrir terminal en VS Code. Navegar a la carpeta raíz:

```powershell
cd "C:\Users\Ing. Kevin\Desktop\PROYECTO AGENTE"
```

Crear la solución:
```powershell
dotnet new sln -n RenergeIA
```
> Nota: En .NET 10 el archivo se crea como `RenergeIA.slnx` (nuevo formato), no `.sln`.

Crear los 3 proyectos:
```powershell
# Proyecto Blazor (capa de presentación)
dotnet new blazor -n RenergeIA.Web -o RenergeIA.Web --framework net10.0 --interactivity Server

# Librería de clases (entidades y dominio)
dotnet new classlib -n RenergeIA.Core -o RenergeIA.Core --framework net10.0

# Librería de clases (EF Core, repositorios)
dotnet new classlib -n RenergeIA.Infrastructure -o RenergeIA.Infrastructure --framework net10.0
```
> Nota: En .NET 10 la plantilla `blazorserver` fue eliminada. El equivalente correcto es `blazor --interactivity Server`.

Agregar proyectos a la solución:
```powershell
dotnet sln add RenergeIA.Web/RenergeIA.Web.csproj
dotnet sln add RenergeIA.Core/RenergeIA.Core.csproj
dotnet sln add RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj
```
> Nota: No especificar el nombre del archivo `.slnx`, dotnet lo detecta solo.

Configurar referencias entre proyectos:
```powershell
dotnet add RenergeIA.Web/RenergeIA.Web.csproj reference RenergeIA.Core/RenergeIA.Core.csproj
dotnet add RenergeIA.Web/RenergeIA.Web.csproj reference RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj
dotnet add RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj reference RenergeIA.Core/RenergeIA.Core.csproj
```

Inicializar Git:
```powershell
git init
```

### 1.7 Verificar que todo funciona

Compilar:
```powershell
dotnet build
```
Resultado esperado: `Compilación correcta. 0 Advertencias, 0 Errores`

Correr la aplicación:
```powershell
dotnet run --project RenergeIA.Web/RenergeIA.Web.csproj
```
Abrir en navegador: `http://localhost:5288`  
Resultado esperado: página Blazor con sidebar (Home, Counter, Weather).

Para detener: `Ctrl + C`

### Checklist Etapa 1 ✓

- [x] .NET SDK 10 instalado
- [x] SQL Server 2025 Developer Edition instalado
- [x] SSMS conectado a `localhost`
- [x] Git instalado y configurado
- [x] VS Code con extensiones C# Dev Kit y mssql
- [x] Solución `RenergeIA.slnx` creada
- [x] 3 proyectos creados y agregados a la solución
- [x] Referencias entre proyectos configuradas
- [x] Git inicializado
- [x] `dotnet build` exitoso
- [x] App corriendo en `http://localhost:5288`

---

## ETAPA 2 — Modelo de Datos (Entidades)

### ¿Qué es esta etapa?
Crear las clases C# que representan los datos del negocio. Estas clases viven en `RenergeIA.Core/Entities/` porque son el núcleo del sistema, sin dependencia de ninguna tecnología externa. Entity Framework Core las usará después para generar las tablas de la base de datos.

### 2.1 Limpiar el proyecto Core

La plantilla `classlib` genera un archivo `Class1.cs` vacío que no se usa. Eliminarlo:

```powershell
# Ejecutar desde la raíz del proyecto
Remove-Item "RenergeIA.Core\Class1.cs" -Force
```

### 2.2 Estructura de carpetas creada

```
RenergeIA.Core/
├── Enums/          ← tipos de datos fijos (listas cerradas de opciones)
└── Entities/       ← clases que representan tablas de la base de datos
```

### 2.3 Enums creados

Los enums son listas cerradas de opciones. Se crean primero porque las entidades los usan.

**Ubicación:** `RenergeIA.Core/Enums/`

| Archivo | Valores | Usado en |
|---|---|---|
| `EstadoProyecto.cs` | Planificacion, EnEjecucion, Suspendido, Completado, Cancelado | Proyecto |
| `EstadoActividad.cs` | Pendiente, EnProgreso, Completada, Retrasada, Bloqueada | ActividadWBS |
| `TipoDocumento.cs` | Plano, Especificacion, Contrato, Permiso, Informe, Procedimiento, Certificado, Otro | Documento |
| `EstadoDocumento.cs` | Borrador, EnRevision, Aprobado, Rechazado, Obsoleto | Documento |
| `TipoPersonal.cs` | Empleado, Contratista, Subcontratista, Visitante | PersonalProyecto |
| `EstadoNoConformidad.cs` | Abierta, EnRevision, EnImplementacion, Cerrada, Cancelada | NoConformidad |
| `SeveridadNoConformidad.cs` | Baja, Media, Alta, Critica | NoConformidad |
| `EstadoRestriccion.cs` | Abierta, EnGestion, Levantada, Cancelada | Restriccion |
| `CategoriaAlerta.cs` | Avance, Costo, Seguridad, Documento, Equipo, Personal, Clima, General | Alerta |
| `TipoEquipo.cs` | Vehiculo, HerramientaMenor, MaquinariaPesada, EquipoMedicion, EquipoSeguridad, Otro | Equipo |
| `TipoMantenimiento.cs` | Preventivo, Correctivo, Predictivo | Mantenimiento |
| `CondicionClimatica.cs` | Soleado, ParcialmenteNublado, Nublado, Lluvia, LluviaFuerte, Tormenta, Viento, Niebla | RegistroClima |

### 2.4 Clase base EntidadBase

**Archivo:** `RenergeIA.Core/Entities/EntidadBase.cs`

Todos los modelos heredan de esta clase para tener `Id`, `FechaCreacion` y `FechaModificacion` sin repetir código.

```csharp
public abstract class EntidadBase
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaModificacion { get; set; }
}
```

### 2.5 Entidades creadas y sus relaciones

**Ubicación:** `RenergeIA.Core/Entities/`

Todas heredan de `EntidadBase`. Orden de creación (por dependencias):

#### Proyecto.cs — entidad central
Todas las demás entidades se relacionan con `Proyecto` directa o indirectamente.
- Campos clave: `Codigo`, `Nombre`, `Cliente`, `Ubicacion`, `Pais`, `CapacidadKWp`, `PresupuestoContractual`, fechas planificadas y reales, `Estado`
- Colecciones: `Actividades`, `InformesDiarios`, `Documentos`, `Partidas`, `NoConformidades`, `Restricciones`, `Personal`, `Equipos`, `Alertas`, `RegistrosClima`

#### PersonalProyecto.cs
Personal en campo (empleados, contratistas, subcontratistas).
- FK: `ProyectoId`
- Campos: `Nombre`, `Apellido`, `DocumentoIdentidad`, `Cargo`, `Empresa`, `TipoPersonal`, fechas de ingreso/salida, `Activo`, `Email`, `Telefono`

#### DocumentoPersona.cs
Documentos del personal (ARL, EPS, cédula, certificados).
- FK: `PersonalProyectoId`
- Campos: `TipoDocumento`, `NombreDocumento`, `RutaArchivo`, fechas de emisión/vencimiento
- Propiedad calculada: `EsVigente` (sin FK, derivada de `FechaVencimiento`)

#### Equipo.cs
Maquinaria y equipos en campo.
- FK: `ProyectoId`
- Campos: `Codigo`, `Nombre`, `TipoEquipo`, `Marca`, `Modelo`, `NumeroSerie`, `Propietario`, fechas, `Activo`

#### RegistroHorometro.cs
Lectura de horas de uso de cada equipo.
- FK: `EquipoId`
- Campos: `Fecha`, `LecturaHorometro`, `HorasTrabajadas`, `Operador`, `Observaciones`

#### Mantenimiento.cs
Mantenimientos preventivos y correctivos de equipos.
- FK: `EquipoId`
- Campos: `TipoMantenimiento`, `Fecha`, `Descripcion`, `Costo`, `RealizadoPor`, `ProximoMantenimiento`

#### ActividadWBS.cs
Actividades del cronograma (Work Breakdown Structure). Soporte para jerarquía (padre-hijo).
- FK: `ProyectoId`, `ActividadPadreId` (nullable, auto-referencia)
- Campos: `CodigoWBS`, `Nombre`, `NivelWBS`, `Responsable`, fechas planificadas y reales, `AvancePlanificado`, `AvanceReal`, `Estado`

#### InformeDiario.cs
Informe diario de campo generado por el residente de obra.
- FK: `ProyectoId`
- Campos: `Fecha`, `NumeroCertificado`, `ResumenActividades`, `PersonalTotal`, `Observaciones`, `CreadoPor`, `Enviado`

#### RegistroAvanceDiario.cs
Avance reportado por actividad dentro de un informe diario.
- FK: `ProyectoId`, `ActividadWBSId`, `InformeDiarioId`
- Campos: `Fecha`, `PorcentajeAvance`, `HorasTrabajadas`, `PersonalEnSitio`, `Observaciones`, `ReportadoPor`

#### Fotografia.cs
Fotos vinculadas a un informe diario.
- FK: `InformeDiarioId`
- Campos: `NombreArchivo`, `RutaArchivo`, `Descripcion`, `FechaToma`, `Latitud`, `Longitud`, `Etiquetas`

#### Documento.cs
Documentos técnicos del proyecto (planos, especificaciones, contratos).
- FK: `ProyectoId`
- Campos: `Codigo`, `Titulo`, `TipoDocumento`, `Estado`, `Disciplina`, `FechaEmision`

#### VersionDocumento.cs
Versiones de cada documento (control de revisiones).
- FK: `DocumentoId`
- Campos: `NumeroVersion`, `RutaArchivo`, `NombreArchivo`, `FechaSubida`, `SubidoPor`, `Comentarios`, `EsVersionActual`

#### Partida.cs
Líneas de presupuesto del proyecto.
- FK: `ProyectoId`, `ActividadWBSId` (nullable)
- Campos: `Codigo`, `Descripcion`, `Unidad`, `Categoria`, `CantidadPresupuestada`, `PrecioUnitario`
- Propiedad calculada: `MontoPresupuestado = Cantidad * PrecioUnitario`

#### CostoReal.cs
Costos reales incurridos contra cada partida.
- FK: `PartidaId`
- Campos: `Fecha`, `Descripcion`, `TipoCosto`, `Cantidad`, `PrecioUnitario`, `NumeroFactura`, `Proveedor`, `RegistradoPor`
- Propiedad calculada: `Monto = Cantidad * PrecioUnitario`

#### NoConformidad.cs
No conformidades de calidad detectadas en campo.
- FK: `ProyectoId`
- Campos: `Numero`, `Titulo`, `Descripcion`, `Categoria`, `Severidad`, `Estado`, `FechaDeteccion`, `FechaCierre`, `DetectadoPor`, `Ubicacion`

#### AccionCorrectiva.cs
Acciones para cerrar una no conformidad.
- FK: `NoConformidadId`
- Campos: `Descripcion`, `Responsable`, `FechaCompromiso`, `FechaImplementacion`, `Estado`, `Observaciones`

#### Restriccion.cs
Restricciones o bloqueos que impiden avanzar en el proyecto.
- FK: `ProyectoId`
- Campos: `Numero`, `Descripcion`, `Tipo`, `Estado`, `FechaIdentificacion`, `FechaCompromiso`, `FechaLevantamiento`, `Responsable`, `Impacto`, `Plan`

#### Alerta.cs
Alertas automáticas del sistema (20 tipos definidos en documentación).
- FK: `ProyectoId`
- Campos: `Categoria`, `Titulo`, `Mensaje`, `Severidad`, `EsLeida`, `DestinatarioId`, `Referencia`

#### RegistroClima.cs
Condiciones climáticas diarias en sitio.
- FK: `ProyectoId`
- Campos: `Fecha`, `Condicion`, `TemperaturaMaxima`, `TemperaturaMinima`, `HumedadRelativa`, `VelocidadViento`, `PrecipitacionMm`, `HorasDisponiblesTrabajar`, `AfectoActividades`, `Observaciones`

### 2.6 Verificación

```powershell
dotnet build RenergeIA.Core/RenergeIA.Core.csproj
```
Resultado esperado: `Compilación correcta. 0 Advertencias, 0 Errores`

### Checklist Etapa 2 — Parte 1 ✓

- [x] `Class1.cs` eliminado
- [x] 12 enums creados en `RenergeIA.Core/Enums/`
- [x] `EntidadBase.cs` creada
- [x] 20 entidades creadas en `RenergeIA.Core/Entities/`
- [x] `dotnet build` de `RenergeIA.Core` exitoso (0 errores)

---

## ETAPA 2 — Parte 2: EF Core, DbContext y base de datos

### ¿Qué es esta parte?
Instalar Entity Framework Core, crear el `DbContext` (la clase que conecta C# con SQL Server), configurar la cadena de conexión y generar la base de datos real con todas las tablas mediante migraciones.

### 2.7 Limpiar Infrastructure

La plantilla `classlib` genera `Class1.cs` vacío. Eliminarlo:
```powershell
Remove-Item "RenergeIA.Infrastructure\Class1.cs" -Force
```

### 2.8 Instalar paquetes NuGet

```powershell
dotnet add RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Tools
dotnet add RenergeIA.Web/RenergeIA.Web.csproj package Microsoft.EntityFrameworkCore.Design
```

| Paquete | Dónde | Para qué |
|---|---|---|
| `EntityFrameworkCore.SqlServer` | Infrastructure | Driver para conectarse a SQL Server |
| `EntityFrameworkCore.Tools` | Infrastructure | Comandos `dotnet ef migrations` |
| `EntityFrameworkCore.Design` | Web | Permite que el proyecto Web ejecute migraciones |

### 2.9 Crear el DbContext

**Archivo:** `RenergeIA.Infrastructure/Data/RenergeIADbContext.cs`

El `DbContext` es la clase central de EF Core: declara qué tablas existen (`DbSet<>`) y configura detalles de cada columna en `OnModelCreating`.

Puntos importantes de la configuración:
- Tipos `decimal` siempre necesitan `HasColumnType("decimal(x,y)")` explícito, si no EF Core genera una advertencia
- `ActividadWBS` tiene auto-referencia (padre-hijo) con `OnDelete(DeleteBehavior.Restrict)` para evitar ciclos
- `RegistroAvanceDiario` tiene 3 FK que llegan a `Proyectos` por distintos caminos — SQL Server no permite múltiples `CASCADE` en ese caso, se usa `DeleteBehavior.Restrict` en `ActividadWBSId` e `InformeDiarioId`
- Propiedades calculadas (`MontoPresupuestado`, `Monto` en `CostoReal`) se ignoran con `e.Ignore(...)` para que EF Core no intente mapearlas como columnas
- `EsVigente` en `DocumentoPersona` es expression-bodied sin setter — EF Core la ignora automáticamente, no hace falta configurar nada

### 2.10 Configurar cadena de conexión

**Archivo:** `RenergeIA.Web/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RenergeIA;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> `TrustServerCertificate=True` es necesario porque SQL Server Developer Edition usa un certificado auto-firmado.

### 2.11 Registrar el DbContext en Program.cs

**Archivo:** `RenergeIA.Web/Program.cs` — agregar antes de `builder.Services.AddRazorComponents()`:

```csharp
using Microsoft.EntityFrameworkCore;
using RenergeIA.Infrastructure.Data;

builder.Services.AddDbContext<RenergeIADbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### 2.12 Instalar dotnet-ef (herramienta global)

Solo se hace una vez por máquina:
```powershell
dotnet tool install --global dotnet-ef
```

Verifica con: `dotnet ef --version`

### 2.13 Crear y aplicar la migración

```powershell
# Crear la migración (genera el código C# que describe las tablas)
dotnet ef migrations add InitialCreate --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj

# Aplicar a SQL Server (crea la base de datos y las tablas)
dotnet ef database update --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj
```

**Si hay que rehacer la migración (por errores):**
```powershell
dotnet ef database drop --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj --force
dotnet ef migrations remove --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj
# corregir el error, luego volver a add + update
```

### Tablas creadas en SQL Server (base de datos: RenergeIA)

| Tabla | Origen |
|---|---|
| Proyectos | Proyecto.cs |
| ActividadesWBS | ActividadWBS.cs |
| InformesDiarios | InformeDiario.cs |
| RegistrosAvanceDiario | RegistroAvanceDiario.cs |
| Fotografias | Fotografia.cs |
| Documentos | Documento.cs |
| VersionesDocumento | VersionDocumento.cs |
| Partidas | Partida.cs |
| CostosReales | CostoReal.cs |
| NoConformidades | NoConformidad.cs |
| AccionesCorrectivas | AccionCorrectiva.cs |
| Restricciones | Restriccion.cs |
| PersonalProyecto | PersonalProyecto.cs |
| DocumentosPersona | DocumentoPersona.cs |
| Equipos | Equipo.cs |
| RegistrosHorometro | RegistroHorometro.cs |
| Mantenimientos | Mantenimiento.cs |
| Alertas | Alerta.cs |
| RegistrosClima | RegistroClima.cs |
| __EFMigrationsHistory | Tabla interna de EF Core (historial de migraciones) |

### Checklist Etapa 2 — Parte 2 ✓

- [x] `Class1.cs` de Infrastructure eliminado
- [x] Paquetes NuGet instalados (SqlServer, Tools, Design)
- [x] `RenergeIADbContext.cs` creado en `RenergeIA.Infrastructure/Data/`
- [x] Cadena de conexión configurada en `appsettings.json`
- [x] DbContext registrado en `Program.cs`
- [x] `dotnet-ef` instalado globalmente
- [x] Migración `InitialCreate` creada sin advertencias
- [x] `dotnet ef database update` exitoso — base de datos `RenergeIA` creada con 20 tablas

---

## ETAPA 3 — ASP.NET Core Identity (Autenticación y Roles)

### ¿Qué es esta etapa?
Agregar el sistema de login, usuarios y roles al proyecto. Identity es la librería oficial de Microsoft para autenticación en ASP.NET Core. Se integra directamente con EF Core y crea sus propias tablas en la base de datos.

### 3.1 Instalar paquetes NuGet

```powershell
dotnet add RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add RenergeIA.Web/RenergeIA.Web.csproj package Microsoft.AspNetCore.Identity.UI
```

### 3.2 Archivos creados en RenergeIA.Infrastructure/Identity/

#### ApplicationUser.cs
Extiende `IdentityUser` con campos propios del negocio:
- `NombreCompleto` — nombre visible del usuario
- `Cargo` — cargo dentro de la empresa
- `Activo` — para deshabilitar usuarios sin borrarlos
- `FechaCreacion` — cuándo fue creado el usuario

#### Roles.cs
Clase estática con las 13 constantes de roles del sistema:

| # | Rol | Descripción |
|---|---|---|
| 1 | Administrador | Acceso total al sistema |
| 2 | DirectorGeneral | Vista ejecutiva de todos los proyectos |
| 3 | GerenteProyecto | Gestión completa de proyectos asignados |
| 4 | IngenierosResidente | Informes diarios, avance, WBS |
| 5 | InspectorCalidad | No conformidades, documentos de calidad |
| 6 | CoordinadorHSE | Seguridad, documentos de personal |
| 7 | AdministradorContrato | Contratos, facturación, costos |
| 8 | JefeAlmacen | Equipos, materiales |
| 9 | SupervisorCampo | Supervisión en sitio |
| 10 | ControlCostos | Presupuesto, partidas, costos reales |
| 11 | Documentador | Gestión documental, versiones |
| 12 | Consultor | Solo lectura |
| 13 | Subcontratista | Acceso limitado para subcontratistas |

#### DatabaseSeeder.cs
Se ejecuta automáticamente al arrancar la app. Crea:
- Los 13 roles si no existen
- Un usuario administrador inicial: `admin@renergeia.com` / `Admin123!`

### 3.3 Cambios en DbContext

`RenergeIADbContext` cambió de heredar `DbContext` a heredar `IdentityDbContext<ApplicationUser>`. Esto hace que EF Core incluya las tablas de Identity en las migraciones.

```csharp
// Antes
public class RenergeIADbContext : DbContext

// Después
public class RenergeIADbContext : IdentityDbContext<ApplicationUser>
```

### 3.4 Configuración en Program.cs

```csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<RenergeIADbContext>()
.AddDefaultTokenProviders();
```

Y en el pipeline HTTP (orden importante):
```csharp
app.UseAuthentication();  // ← primero autenticación
app.UseAuthorization();   // ← luego autorización
app.UseAntiforgery();
```

Y el seeder al final antes de `app.Run()`:
```csharp
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await DatabaseSeeder.SeedRolesAndAdminAsync(roleManager, userManager);
}
```

### 3.5 Migración de Identity

```powershell
dotnet ef migrations add AddIdentity --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj
dotnet ef database update --project RenergeIA.Infrastructure/RenergeIA.Infrastructure.csproj --startup-project RenergeIA.Web/RenergeIA.Web.csproj
```

Tablas creadas por Identity:
- `AspNetUsers` — usuarios (con campos personalizados)
- `AspNetRoles` — roles
- `AspNetUserRoles` — qué rol tiene cada usuario
- `AspNetUserClaims` — claims de usuarios
- `AspNetRoleClaims` — claims de roles
- `AspNetUserLogins` — logins externos (Google, etc.)
- `AspNetUserTokens` — tokens de seguridad

### Verificación en SSMS

```sql
USE RenergeIA;
SELECT Name FROM AspNetRoles ORDER BY Name;
-- Resultado: 13 filas con todos los roles
```

### Checklist Etapa 3 ✓

- [x] Paquetes Identity instalados
- [x] `ApplicationUser.cs` creado con campos personalizados
- [x] `Roles.cs` con 13 constantes de roles
- [x] `DatabaseSeeder.cs` que crea roles y admin al arrancar
- [x] `RenergeIADbContext` modificado a `IdentityDbContext<ApplicationUser>`
- [x] Identity configurado en `Program.cs`
- [x] `UseAuthentication()` y `UseAuthorization()` agregados al pipeline
- [x] Migración `AddIdentity` aplicada — 7 tablas `AspNetXxx` creadas
- [x] Seeder ejecutado — 13 roles en `AspNetRoles` confirmados en SSMS
- [x] Usuario admin creado: `admin@renergeia.com` / `Admin123!`

---

## ETAPA 4 — Página de Login y Verificación de Autenticación

**Objetivo:** Crear la interfaz de login, conectarla con Identity, y verificar que el flujo completo (login → home → logout) funcione correctamente.

### 4.1 Archivos creados

Se crearon los siguientes componentes Blazor SSR (sin `@rendermode`, para que puedan acceder a `HttpContext` y manejar cookies):

| Archivo | Propósito |
|---|---|
| `RenergeIA.Web/Components/Pages/Auth/Login.razor` | Página de inicio de sesión |
| `RenergeIA.Web/Components/Pages/Auth/Logout.razor` | Cierra sesión y redirige |
| `RenergeIA.Web/Components/RedirectToLogin.razor` | Redirige al login si no autenticado |

### 4.2 Login.razor — Página de inicio de sesión

**Características:**
- Ruta: `/login`
- Layout propio: `@layout LoginLayout` (sin sidebar)
- Detecta si ya está autenticado y redirige a `/`
- Al recibir POST, lee credenciales de `HttpContext.Request.Form`
- Usa `SignInManager.PasswordSignInAsync` para validar
- Muestra mensaje de error si las credenciales son incorrectas
- Mantiene el correo en el campo si falla el login

**Puntos técnicos clave:**

```razor
<form method="post" @formname="login-form">
    <AntiforgeryToken />
    ...
</form>
```

> Por qué los dos atributos:
> - `@formname="login-form"` — Blazor SSR lo **exige** para identificar cuál form se está enviando
> - `<AntiforgeryToken />` — En .NET 10, `@formname` NO inyecta el token automáticamente; hay que agregarlo explícito
>
> Si falta `@formname` → error "The POST request does not specify which form is being submitted"
> Si falta `<AntiforgeryToken />` → error "A valid antiforgery token was not provided with the request"

Lectura de datos del form (sin `[SupplyParameterFromForm]`, directo desde HttpContext):

```csharp
if (HttpMethods.IsPost(HttpContext.Request.Method))
{
    var form = HttpContext.Request.Form;
    var email = form["email"].ToString();
    var password = form["password"].ToString();
    var rememberMe = form["rememberMe"].ToString() == "on";

    var result = await SignInManager.PasswordSignInAsync(
        email, password, rememberMe, lockoutOnFailure: false);

    if (result.Succeeded)
        Navigation.NavigateTo("/", replace: true);
    else
        ErrorMessage = "Correo o contraseña incorrectos. Intenta de nuevo.";
}
```

### 4.3 Logout.razor — Cerrar sesión

```csharp
protected override async Task OnInitializedAsync()
{
    await SignInManager.SignOutAsync();
    Navigation.NavigateTo("/login", replace: true);
}
```

### 4.4 RedirectToLogin.razor — Componente de redirección

Usado en `Routes.razor` dentro del bloque `<NotAuthorized>`. Navega a `/login` en `OnInitialized`.

### 4.5 Routes.razor — Protección de rutas

```razor
<CascadingAuthenticationState>
    <Router AppAssembly="typeof(Program).Assembly">
        <Found Context="routeData">
            <AuthorizeRouteView RouteData="routeData" DefaultLayout="typeof(MainLayout)">
                <NotAuthorized>
                    <RedirectToLogin />
                </NotAuthorized>
            </AuthorizeRouteView>
        </Found>
    </Router>
</CascadingAuthenticationState>
```

### 4.6 LoginLayout.razor — Layout sin sidebar para la página de login

```razor
@inherits LayoutComponentBase
@Body
```

### 4.7 MainLayout.razor — Muestra usuario y botón Salir

```razor
<AuthorizeView>
    <Authorized>
        <span class="text-muted small">@context.User.Identity?.Name</span>
        <a href="/logout" class="btn btn-sm btn-outline-secondary">Salir</a>
    </Authorized>
</AuthorizeView>
```

### 4.8 Home.razor — Página protegida

```razor
@page "/"
@attribute [Authorize]
```

El atributo `[Authorize]` hace que cualquier usuario no autenticado sea redirigido automáticamente al login.

### 4.9 Configuración del cookie path en Program.cs

Por defecto, Identity redirige a `/Account/Login`. Para cambiar la ruta:

```csharp
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/login";
});
```

### 4.10 Errores encontrados y soluciones

| Error | Causa | Solución |
|---|---|---|
| `/Account/Login` Not Found | Identity usa esa ruta por defecto | `ConfigureApplicationCookie` con `LoginPath = "/login"` |
| "A valid antiforgery token was not provided" | `@formname` en .NET 10 no inyecta el token automáticamente | Agregar `<AntiforgeryToken />` explícito dentro del form |
| "The POST request does not specify which form" | Se quitó `@formname` al intentar el fix anterior | Mantener `@formname` Y agregar `<AntiforgeryToken />` — los dos son necesarios |
| Warning RZ10012 al compilar | Advertencia de que `<AntiforgeryToken />` no debería usarse con `@formname` | Es solo una advertencia, no un error. Se ignora — el comportamiento es correcto |

### 4.11 Flujo completo verificado

```
Usuario no autenticado accede a /
    → [Authorize] detecta que no hay sesión
    → CascadingAuthenticationState + AuthorizeRouteView
    → <NotAuthorized> → RedirectToLogin → navega a /login

Usuario ingresa admin@renergeia.com / Admin123! y hace clic en Ingresar
    → POST a /login con token antiforgery válido
    → SignInManager.PasswordSignInAsync → Succeeded
    → Cookie de sesión creada
    → Navigation.NavigateTo("/") → Home

Home muestra "Bienvenido a RenergeIA" + email en top-right

Usuario hace clic en "Salir"
    → Navega a /logout
    → SignInManager.SignOutAsync() → cookie eliminada
    → Navigation.NavigateTo("/login")
```

### Checklist Etapa 4 ✓

- [x] `LoginLayout.razor` creado (layout sin sidebar)
- [x] `Login.razor` creado con diseño visual (fondo degradado oscuro, tarjeta centrada)
- [x] `Logout.razor` creado
- [x] `RedirectToLogin.razor` creado
- [x] `Routes.razor` actualizado con `CascadingAuthenticationState` y `AuthorizeRouteView`
- [x] `MainLayout.razor` actualizado con nombre de usuario y botón Salir
- [x] `Home.razor` protegido con `[Authorize]`
- [x] Cookie path configurado (`/login`, `/logout`)
- [x] Antiforgery resuelto: `@formname` + `<AntiforgeryToken />` explícito
- [x] Login verificado: `admin@renergeia.com` / `Admin123!` → redirige a Home
- [x] Logout verificado: botón "Salir" → redirige a Login
- [x] Rutas protegidas: acceso directo a `/` sin sesión → redirige a `/login`

---

## ETAPA 5 — Módulo de Proyectos (CRUD completo)

**Objetivo:** Construir las 4 operaciones básicas sobre Proyectos: Listar, Crear, Ver detalle, Editar y Eliminar.

### 5.1 Archivos creados

| Archivo | Ruta | Propósito |
|---|---|---|
| `ListaProyectos.razor` | `Pages/Proyectos/` | Lista de proyectos con tarjetas |
| `NuevoProyecto.razor` | `Pages/Proyectos/` | Formulario de creación |
| `DetalleProyecto.razor` | `Pages/Proyectos/` | Vista de detalle + botones Editar/Eliminar |
| `EditarProyecto.razor` | `Pages/Proyectos/` | Formulario de edición |

### 5.2 Cambios en archivos existentes

**`_Imports.razor`** — se agregaron los `@using` globales para no repetirlos en cada página:
```razor
@using RenergeIA.Core.Entities
@using RenergeIA.Core.Enums
@using RenergeIA.Infrastructure.Data
@using Microsoft.EntityFrameworkCore
@using System.ComponentModel.DataAnnotations
```

**`NavMenu.razor`** — se agregó el enlace a Proyectos y se corrigió el nombre de la app a "RenergeIA".

### 5.3 Patrones usados en este módulo

**Inyección del DbContext directamente en el componente:**
```razor
@inject RenergeIADbContext Db
```
En Blazor Server es válido inyectar el DbContext directo porque cada circuito Blazor tiene su propio scope. No se necesita Repository ni Service para operaciones simples.

**Modo interactivo para componentes con lógica:**
```razor
@rendermode InteractiveServer
```
A diferencia del Login (que es SSR puro para manejar cookies), los módulos usan `InteractiveServer` para poder usar `@onclick`, `EditForm`, binding reactivo, etc.

**Validaciones con DataAnnotations:**
```csharp
private sealed class FormModel
{
    [Required(ErrorMessage = "El código es obligatorio")]
    [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
    public string Codigo { get; set; } = string.Empty;
    ...
}
```
Se usa una clase `FormModel` separada de la entidad para no exponer la entidad directamente al formulario. Esto evita problemas de tracking de EF Core.

**Confirmación de eliminación sin modal — inline:**
```razor
@if (_confirmarEliminar)
{
    <div class="alert alert-danger ...">
        ¿Seguro que deseas eliminar...?
        <button @onclick="EliminarAsync">Sí, eliminar</button>
        <button @onclick="() => _confirmarEliminar = false">Cancelar</button>
    </div>
}
```
No se usó un modal de Bootstrap para mantener la simplicidad. El mensaje aparece en la misma página.

**Emojis en strings de C# — usar carácter directo, no entidades HTML:**
```csharp
// MAL: Blazor trata el string como texto plano, muestra "&#128196;" literal
("WBS / Actividades", "&#128196;", false)

// BIEN: usar el emoji directamente en el string
("WBS / Actividades", "📄", false)
```

### 5.4 Flujo completo verificado

```
/proyectos          → Lista de proyectos (tarjetas con badge de color por estado)
/proyectos/nuevo    → Formulario con validaciones → al guardar → /proyectos/{id}
/proyectos/{id}     → Detalle completo + botones Editar / Eliminar
/proyectos/{id}/editar → Formulario pre-cargado → al guardar → /proyectos/{id}
Eliminar            → Confirmación inline → elimina → /proyectos
```

### Checklist Etapa 5 ✓

- [x] `ListaProyectos.razor` — tarjetas con badge de estado por color
- [x] `NuevoProyecto.razor` — formulario con validaciones, redirige al detalle al guardar
- [x] `DetalleProyecto.razor` — info general, cronograma, módulos del proyecto (futuros)
- [x] `EditarProyecto.razor` — formulario pre-cargado con datos actuales, incluye fechas reales
- [x] Eliminación con confirmación inline (sin modal)
- [x] `_Imports.razor` actualizado con usings globales de Core, EF Core y DataAnnotations
- [x] NavMenu actualizado con enlace a Proyectos
- [x] Creación, edición y eliminación probadas con datos reales en base de datos

---

## Cómo correr el proyecto en cualquier momento

```powershell
cd "C:\Users\Ing. Kevin\Desktop\PROYECTO AGENTE"
dotnet run --project RenergeIA.Web/RenergeIA.Web.csproj
```
Abrir: `http://localhost:5288`  
Detener: `Ctrl + C`
