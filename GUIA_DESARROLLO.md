






# RenergeIA — Guía de Desarrollo Paso a Paso

Registro completo de todo lo construido: qué se hizo, por qué, en qué orden y cómo.  
Actualizar este archivo cada vez que se avance en una nueva etapa o paso.

---

## Índice de contenidos

| Sección | Descripción |
|---|---|
| [¿Cómo obtener el proyecto desde GitHub?](#cómo-obtener-el-proyecto-desde-github) | Clonar, instalar y arrancar en un equipo nuevo |
| [Hoja de ruta del proyecto](#hoja-de-ruta-del-proyecto) | Estado de todas las etapas planeadas |
| [Etapa 0 — Decisiones y contexto](#etapa-0--decisiones-previas-y-contexto-del-proyecto) | Tecnologías, arquitectura, fases |
| [Etapa 1 — Preparación del entorno](#etapa-1--preparación-del-entorno) | Instalación de herramientas, extensiones VS Code |
| [Etapa 2 — Modelo de datos](#etapa-2--modelo-de-datos-entidades) | Entidades, enums, relaciones |
| [Etapa 2 Parte 2 — EF Core y base de datos](#etapa-2--parte-2-ef-core-dbcontext-y-base-de-datos) | DbContext, migraciones, SQL Server |
| [Etapa 3 — Identity y roles](#etapa-3--aspnet-core-identity-autenticación-y-roles) | Login, 13 roles, seeder |
| [Etapa 4 — Login UI](#etapa-4--página-de-login-y-verificación-de-autenticación) | Página de login, logout, protección de rutas |
| [Etapa 5 — Módulo Proyectos](#etapa-5--módulo-de-proyectos-crud-completo) | CRUD completo de proyectos |
| [Etapa 6 — Módulo Personal](#etapa-6--módulo-de-personal-del-proyecto-crud-completo) | CRUD de personal vinculado a proyectos |
| [Etapa 7 — Módulo Equipos](#etapa-7--módulo-de-equipos-crud-completo) | Maquinaria, horómetros, mantenimientos |
| [Etapa 8 — Módulo WBS](#etapa-8--módulo-wbs--estructura-de-actividades) | Árbol de tareas, plantilla tipo EPC fotovoltaico |
| [Etapa 9 — Módulo Informe Diario](#etapa-9--módulo-informe-diario--avance-diario) | Análisis arquitectónico, transformación Excel → Web |
| [Cómo correr el proyecto](#cómo-correr-el-proyecto-en-cualquier-momento) | Comando para arrancar la app |

---

## Hoja de ruta del proyecto

Estado de cada etapa de desarrollo. Se actualiza al completar cada una.

### Fase 1 — Núcleo del sistema (Módulos base)

| # | Etapa | Descripción | Estado |
|---|---|---|---|
| 0 | Contexto y decisiones | Tecnologías, arquitectura, fases del proyecto | ✅ Completo |
| 1 | Entorno | .NET 10, SQL Server, VS Code, extensiones | ✅ Completo |
| 2 | Modelo de datos | 20 entidades, 12 enums, EF Core, migraciones | ✅ Completo |
| 3 | Identity y roles | Login, 13 roles, seeder de admin | ✅ Completo |
| 4 | Login UI | Página de login, logout, rutas protegidas | ✅ Completo |
| 5 | Proyectos | CRUD completo (listar, crear, ver, editar, eliminar) | ✅ Completo |
| 6 | Personal | CRUD de personas por proyecto, filtros, activo/inactivo | ✅ Completo |
| 7 | Equipos | CRUD de maquinaria por proyecto | ✅ Completo |
| 8 | WBS / Actividades | Árbol de tareas, plantilla tipo EPC, activar/desactivar | ✅ Completo |
| 9 | Informe Diario | Registro diario de avance por actividad | ✅ Completo |
| 10 | Documentos | Gestión documental con versiones | ⏳ Pendiente |
| 11 | Costos | Partidas presupuestales y costos reales | ⏳ Pendiente |
| 12 | No Conformidades | Registro de NC y acciones correctivas | ⏳ Pendiente |
| 13 | Restricciones | Registro y seguimiento de restricciones | ⏳ Pendiente |
| 14 | Alertas | Sistema de alertas automáticas | ⏳ Pendiente |
| 15 | Dashboard | Inicio con métricas y resumen ejecutivo | ⏳ Pendiente |

### Fase 2 — Inteligencia y conectividad (futuro)

| # | Etapa | Descripción | Estado |
|---|---|---|---|
| 16 | Dashboard gerencial avanzado | KPIs, gráficas, exportación PDF | ⏳ Fase 2 |
| 17 | IA con OpenAI API | Predicción de atrasos, análisis de riesgo | ⏳ Fase 2 |
| 18 | Reportes automáticos | Generación de informes PDF con QuestPDF | ⏳ Fase 2 |

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

## ETAPA 6 — Módulo de Personal del Proyecto (CRUD completo)

**Objetivo:** Gestionar las personas vinculadas a cada proyecto: empleados, contratistas, subcontratistas y visitantes.

### 6.1 Archivos creados

| Archivo | Ruta | Propósito |
|---|---|---|
| `ListaPersonal.razor` | `Pages/Personal/` | Tabla con filtros de búsqueda, tipo y estado |
| `FormPersonal.razor` | `Pages/Personal/` | Formulario único para crear y editar |

### 6.2 Rutas del módulo

```
/proyectos/{id}/personal                    → Lista de personal del proyecto
/proyectos/{id}/personal/nuevo              → Formulario de creación
/proyectos/{id}/personal/{personaId}/editar → Formulario de edición
```

El módulo siempre está vinculado a un proyecto: el `ProyectoId` viaja en la URL, no hay personal "suelto".

### 6.3 Patrón: un formulario para crear y editar

En lugar de tener dos archivos separados (`NuevoPersonal.razor` y `EditarPersonal.razor`), se usa un solo componente con dos rutas:

```razor
@page "/proyectos/{ProyectoId:int}/personal/nuevo"
@page "/proyectos/{ProyectoId:int}/personal/{PersonalId:int}/editar"
```

El componente detecta si está en modo crear o editar:
```csharp
private bool _esNuevo => PersonalId is null;
```

Esto reduce duplicación de código. El mismo formulario, los mismos campos, la misma validación — solo cambia si se hace `Add` o se actualiza el registro existente.

### 6.4 Filtros reactivos en la lista

Los filtros funcionan sin botón "Buscar" — se actualizan al instante usando una propiedad calculada:

```csharp
private IEnumerable<PersonalProyecto> PersonalFiltrado => _personal
    .Where(p =>
        (string.IsNullOrEmpty(_busqueda) || $"{p.Nombre} {p.Apellido} {p.Cargo}".Contains(_busqueda, ...)) &&
        (string.IsNullOrEmpty(_filtroTipo) || p.TipoPersonal.ToString() == _filtroTipo) &&
        (string.IsNullOrEmpty(_filtroActivo) || p.Activo.ToString() == _filtroActivo)
    );
```

El filtro de estado viene en `"true"` por defecto para mostrar solo activos. Cambiarlo a `""` muestra todos.

### 6.5 Módulo Personal activado en DetalleProyecto

`DetalleProyecto.razor` se actualizó para que los módulos disponibles sean clicables. La lista de módulos ahora incluye la URL de destino:

```csharp
_modulos =
[
    ("Personal",          "👷", true,  $"/proyectos/{Id}/personal"),  // ← disponible
    ("WBS / Actividades", "📄", false, null),                          // ← próximamente
    ...
];
```

### Checklist Etapa 6 ✓

- [x] `ListaPersonal.razor` — tabla con búsqueda por texto, filtro por tipo y por estado activo/inactivo
- [x] `FormPersonal.razor` — un solo formulario para crear y editar (dos rutas `@page`)
- [x] Vinculación correcta por `ProyectoId` en la URL
- [x] Eliminación con confirmación inline (igual que en Proyectos)
- [x] Badge de color por tipo: Empleado (azul), Contratista (celeste), Subcontratista (amarillo), Visitante (gris)
- [x] Estado Activo/Inactivo reflejado en tiempo real en la tabla
- [x] Módulo Personal clicable desde el detalle del proyecto
- [x] Creación, edición y cambio de estado probados con datos reales

---

## ETAPA 7 — Módulo de Equipos y Maquinaria (CRUD completo)

**Objetivo:** Registrar toda la maquinaria, vehículos y herramientas vinculadas a cada proyecto, con control de estado activo/inactivo y filtros.

### 7.1 Archivos creados

| Archivo | Ruta | Propósito |
|---|---|---|
| `ListaEquipos.razor` | `Pages/Equipos/` | Tabla con filtros por tipo y estado |
| `FormEquipo.razor` | `Pages/Equipos/` | Formulario único para crear y editar |

### 7.2 Rutas del módulo

```
/proyectos/{id}/equipos                     → Lista de equipos del proyecto
/proyectos/{id}/equipos/nuevo               → Formulario de creación
/proyectos/{id}/equipos/{equipoId}/editar   → Formulario de edición
```

### 7.3 Tipos de equipo y colores de badge

| Tipo | Badge | Ejemplos |
|---|---|---|
| Vehículo | Azul | Camioneta, volqueta, bus de personal |
| Maquinaria Pesada | Rojo | Retroexcavadora, minicargador, grúa |
| Herramienta Menor | Amarillo | Taladro, esmeril, compresor |
| Equipo de Medición | Celeste | Nivel, teodolito, multímetro |
| Equipo de Seguridad | Verde | Arnés, detector de gases |
| Otro | Gris | Cualquier otro equipo |

### 7.4 Módulo Equipos activado en DetalleProyecto

```csharp
("Equipos", "⚙️", true, $"/proyectos/{Id}/equipos"),
```

### Checklist Etapa 7 ✅

- [x] `ListaEquipos.razor` — tabla con búsqueda por texto, filtro por tipo y estado
- [x] `FormEquipo.razor` — formulario único para crear y editar (dos rutas `@page`)
- [x] Vinculación por `ProyectoId` en la URL
- [x] Badge de color por tipo de equipo (6 tipos)
- [x] Estado Activo/Inactivo con filtro reactivo
- [x] Eliminación con confirmación inline
- [x] Módulo Equipos clicable desde el detalle del proyecto
- [x] Creación, edición y cambio de estado probados con datos reales

---

## ETAPA 8 — Módulo WBS / Estructura de Actividades

**Objetivo:** Registrar la estructura de desglose de trabajo (WBS) de cada proyecto en forma de árbol jerárquico, con soporte para cargar una plantilla predeterminada basada en el cronograma tipo EPC fotovoltaico de Renergeia.

### 8.1 Archivos creados o modificados

| Archivo | Ruta | Cambio |
|---|---|---|
| `ListaWBS.razor` | `Pages/WBS/` | Vista árbol + plantilla + toggle activo/inactivo |
| `FormWBS.razor` | `Pages/WBS/` | Formulario crear/editar, incluye campo Activo |
| `ActividadWBS.cs` | `RenergeIA.Core/Entities/` | Se agregó propiedad `bool Activo` |

### 8.2 Rutas del módulo

```
/proyectos/{id}/wbs                          → Árbol de actividades del proyecto
/proyectos/{id}/wbs/nueva                    → Formulario de nueva actividad
/proyectos/{id}/wbs/nueva?padreId=X          → Nueva subactividad bajo la actividad X
/proyectos/{id}/wbs/{actividadId}/editar     → Editar actividad existente
```

### 8.3 Migración requerida

Al integrar este módulo en un equipo nuevo, la base de datos necesita una migración adicional para la columna `Activo` en la tabla `ActividadesWBS`. Ejecutar **después** de `dotnet ef database update` inicial:

```powershell
dotnet ef migrations add AddActivoWBS --project RenergeIA.Infrastructure --startup-project RenergeIA.Web
dotnet ef database update --project RenergeIA.Infrastructure --startup-project RenergeIA.Web
```

> Si al clonar el repo ya existe esa migración en el historial, el comando `dotnet ef database update` la aplica automáticamente y no hay que crearla de nuevo.

### 8.4 Funcionalidades implementadas

#### Vista árbol (ListaWBS.razor)

- Las actividades se muestran con **indentación visual** según su nivel WBS (1.1, 1.1.1, 1.1.1.1...)
- Orden **numérico correcto** del código WBS: 1.1 → 1.2 → ... → 1.9 → 1.10 → 1.11
- Tarjetas de resumen: actividades activas, completadas, en progreso, con problemas
- Toggle **"Mostrar inactivas"** para ver/ocultar actividades desactivadas
- Cada actividad tiene botón **"✓ Activa" / "Inactiva"** para activar o desactivar al instante
- Botón **"+Sub"** que abre el formulario de nueva subactividad con el padre ya seleccionado

#### Plantilla tipo EPC fotovoltaico

Al crear el primer WBS de un proyecto, aparecen dos opciones:

```
[📋 Cargar plantilla]   [+ Nueva actividad manual]
```

La plantilla carga **110 actividades** organizadas en 4 niveles jerárquicos:

| Sección | Descripción |
|---|---|
| 1.1 | Hitos Principales del Contrato (firma EPC, pago anticipo...) |
| 1.2 | Hitos Generales del Contrato |
| 1.3 | Permisos de construcción |
| 1.4 | Estudios de Ingeniería |
| 1.5 | Pull Out Test |
| 1.6 | Ingeniería de detalle |
| 1.7 | Suministros (módulos, inversores, cables MT/BT/Solar, SCADA, CCTV...) |
| 1.8 | Construcción Planta (obras civiles, mecánicas, eléctricas, pruebas...) |
| 1.9 | Culminación Sustancial |
| 1.10 | Cierre |
| 1.11 | Aceptación Provisional |

**Las fechas se calculan automáticamente** desde la fecha de inicio planificada del proyecto usando los desfases del cronograma original (proyecto Hato Grande). Después de cargar la plantilla, el usuario puede:
- Desactivar las actividades que no apliquen a este proyecto
- Editar fechas, responsables o avance en cada actividad
- Agregar nuevas actividades manualmente

#### Campo Activo en la entidad

Se agregó `bool Activo = true` a `ActividadWBS`. Las actividades inactivas no se borran: quedan en la base de datos pero se filtran de la vista y de los contadores de resumen. Esto permite reactivarlas si vuelven a necesitarse.

### 8.5 Truco de ordenamiento WBS

El código WBS "1.10" se ordena después de "1.2" si se usa orden alfabético. Se resuelve con un método que convierte el código a clave con ceros a la izquierda:

```csharp
private static string WbsSortKey(string codigo) =>
    string.Join(".", codigo.Split('.').Select(p => p.PadLeft(4, '0')));
// "1.10" → "0001.0010"  |  "1.2" → "0001.0002"
// Resultado: 1.1 → 1.2 → ... → 1.9 → 1.10 → 1.11 ✓
```

### 8.6 Carga de la plantilla: algoritmo nivel por nivel

Para insertar las ~110 actividades manteniendo la relación padre-hijo:

```csharp
// Se insertan por nivel (1 → 2 → 3 → 4)
// Después de cada SaveChanges, EF Core llena el Id generado
// Se mapea código WBS → Id en un diccionario para asignarlo a los hijos

for (int nivel = 1; nivel <= 4; nivel++)
{
    var batch = _plantilla.Where(p => p.Nivel == nivel).ToList();
    var entities = new List<(string Codigo, ActividadWBS Entity)>();

    foreach (var item in batch)
    {
        int? padreId = item.CodigoPadre is not null && idMap.TryGetValue(item.CodigoPadre, out var pid)
            ? pid : null;

        var act = new ActividadWBS { ..., ActividadPadreId = padreId };
        Db.ActividadesWBS.Add(act);
        entities.Add((item.Codigo, act));
    }

    await Db.SaveChangesAsync(); // ← IDs quedan en act.Id

    foreach (var (codigo, entity) in entities)
        idMap[codigo] = entity.Id; // ← disponible para el siguiente nivel
}
```

### Checklist Etapa 8 ✅

- [x] `ListaWBS.razor` — árbol con indentación, orden numérico, filtros, toggle activo
- [x] `FormWBS.razor` — crear/editar actividad, campo Activo, slider de avance
- [x] Campo `Activo` en entidad `ActividadWBS` + migración `AddActivoWBS`
- [x] Plantilla con 110 actividades tipo EPC fotovoltaico (base: proyecto Hato Grande)
- [x] Cálculo automático de fechas desde el inicio planificado del proyecto
- [x] Botón "+Sub" que pasa `?padreId=X` para crear subactividades enlazadas
- [x] Toggle activa/inactiva por fila sin recargar la página
- [x] Estado vacío con opciones "Cargar plantilla" o "Nueva actividad manual"
- [x] Módulo WBS clicable desde el detalle del proyecto

---

## ETAPA 9 — Módulo Informe Diario / Avance Diario

**Estado:** 📋 Análisis y diseño arquitectónico en progreso  
**Fecha inicio:** 08/06/2026

### 9.1 Contexto y desafío

El Informe Diario es **el módulo central del sistema** — aquí se registra el avance real de las actividades, se compara contra lo programado, se generan alertas y se alimentan los indicadores del proyecto.

**Desafío principal:** La empresa actualmente usa una **plantilla Excel compleja** con múltiples hojas interconectadas mediante fórmulas. El objetivo NO es replicar el Excel como página web, sino **extraer la lógica funcional** y convertirla en un sistema web moderno.

### 9.2 Principio rector del módulo

> **El cronograma/WBS es la fuente maestra.**

- El Informe Diario **NO permite crear actividades nuevas**
- Las actividades del Informe Diario provienen exclusivamente del módulo WBS
- Solo las actividades **activas** (`Activo = true`) aparecen en el informe
- Si una actividad se desactiva, desaparece de futuros informes (los históricos se conservan)

### 9.3 Flujo funcional del módulo

```
1. Usuario accede a "Informes Diarios" de un Proyecto
   ↓
2. Crea nuevo Informe Diario para una fecha específica
   ↓
3. Sistema carga automáticamente:
   - Actividades WBS ACTIVAS del proyecto
   - Para cada actividad crea un RegistroAvanceDiario vacío
   - Calcula AvanceEsperado según fechas planificadas
   ↓
4. Usuario registra por cada actividad:
   - Cantidad ejecutada del día
   - Porcentaje de avance (o se calcula automáticamente)
   - Personal asignado + horas
   - Equipos utilizados + horas
   - Restricciones encontradas
   - Fotografías
   - Observaciones
   ↓
5. Sistema calcula automáticamente:
   - Avance esperado vs avance real
   - Desviación
   - Días de atraso
   - Estado (Al día / Atrasado / Adelantado)
   - Genera alertas si desviación > 5%
   ↓
6. Usuario registra información general:
   - Clima del día
   - Restricciones generales
   - Observaciones del proyecto
   - Fotografías generales
   ↓
7. Usuario cambia estado:
   Borrador → En Revisión
   ↓
8. Director de Proyecto revisa:
   - Aprueba → Informe bloqueado
   - Rechaza → Vuelve a Borrador con observaciones
   ↓
9. Informe Aprobado alimenta:
   - Curva S
   - Dashboard del proyecto
   - Reportes
   - Indicadores SPI
```

### 9.4 Diagnóstico del estado actual

#### Entidades existentes que se reutilizan ✅

- `InformeDiario` — base creada
- `RegistroAvanceDiario` — base creada
- `ActividadWBS` — completa con fechas planificadas
- `Proyecto` — base completa
- `PersonalProyecto` — completa
- `Equipo` — completo
- `RegistroClima` — completo
- `Restriccion` — completa
- `Fotografia` — completa
- `Alerta` — base creada

#### Campos faltantes identificados ⚠️

**En `InformeDiario` (7 campos):**
```csharp
public EstadoInforme Estado { get; set; }          // Borrador, EnRevision, Aprobado, Rechazado, Anulado
public string? RevisadoPor { get; set; }           // Director que revisa
public DateTime? FechaRevision { get; set; }
public string? MotivoRechazo { get; set; }
public int Version { get; set; } = 1;              // Trazabilidad
public string? ComentariosGenerales { get; set; }
public int? InformeDiarioAnteriorId { get; set; }  // Versionado
```

**En `RegistroAvanceDiario` (8 campos + relaciones M:N):**
```csharp
public decimal CantidadEjecutadaDia { get; set; }  // Cantidad física del día
public decimal AvanceEsperado { get; set; }        // Calculado automáticamente
public decimal AvanceAcumulado { get; set; }       // Suma de todos los avances
public decimal Desviacion { get; set; }            // Real - Esperado
public int DiasAtraso { get; set; }
public EstadoAvance Estado { get; set; }           // AlDia, Atrasado, Adelantado
public string? Novedades { get; set; }
public decimal? HorasAfectadasClima { get; set; }

// Relaciones muchos a muchos:
public ICollection<PersonalProyecto> PersonalAsignado { get; set; }
public ICollection<Equipo> EquiposUtilizados { get; set; }
public ICollection<Restriccion> RestriccionesRelacionadas { get; set; }
```

**En `ActividadWBS` (6 campos para cálculos):**
```csharp
public decimal CantidadTotal { get; set; }              // Cantidad total planificada
public string Unidad { get; set; }                      // m, kg, und, global, etc.
public decimal CantidadEjecutadaAcumulada { get; set; }
public bool EsCritica { get; set; }                     // Ruta crítica
public string? Disciplina { get; set; }                 // Civil, Eléctrico, Mecánico
public string? FrenteTrabajo { get; set; }
```

#### Nuevas entidades requeridas

**Enums:**
```csharp
public enum EstadoInforme { Borrador, EnRevision, Aprobado, Rechazado, Anulado }
public enum EstadoAvance { AlDia, Atrasado, Adelantado }
```

**Tablas intermedias (M:N):**
```csharp
// RegistroAvancePersonal — qué personal trabajó en cada actividad
public class RegistroAvancePersonal
{
    public int RegistroAvanceDiarioId { get; set; }
    public int PersonalProyectoId { get; set; }
    public decimal HorasTrabajadas { get; set; }
}

// RegistroAvanceEquipo — qué equipos se usaron
public class RegistroAvanceEquipo
{
    public int RegistroAvanceDiarioId { get; set; }
    public int EquipoId { get; set; }
    public decimal HorasUtilizadas { get; set; }
}

// RegistroAvanceRestriccion — restricciones que afectaron la actividad
public class RegistroAvanceRestriccion
{
    public int RegistroAvanceDiarioId { get; set; }
    public int RestriccionId { get; set; }
}
```

### 9.5 Cálculos automáticos requeridos

#### Avance esperado
```csharp
// Fórmula:
AvanceEsperado = ((FechaInforme - FechaInicio) / (FechaFin - FechaInicio)) * 100

// Ejemplo:
// Actividad: Hincado | Inicio: 01/06/2026 | Fin: 30/06/2026 | Informe: 15/06/2026
// Días totales: 30 | Días transcurridos: 15
// Avance esperado: (15/30) * 100 = 50%
```

#### Avance real
```csharp
// Opción 1: Por cantidad
AvanceReal = (CantidadEjecutadaAcumulada / CantidadTotal) * 100

// Opción 2: Por porcentaje directo (actividades sin cantidad medible)
AvanceReal = AvanceReportado
```

#### Desviación y estado
```csharp
Desviacion = AvanceReal - AvanceEsperado

if (Desviacion >= 0) 
    Estado = EstadoAvance.AlDia o Adelantado
else if (Desviacion < -5) 
{
    Estado = EstadoAvance.Atrasado;
    // GENERAR ALERTA AUTOMÁTICA
}
```

#### Indicador SPI (Schedule Performance Index)
```csharp
SPI = AvanceReal / AvanceEsperado

// Interpretación:
// SPI >= 1.0  → Al día o adelantado
// 0.95-1.0    → Leve atraso
// SPI < 0.95  → Atraso significativo
```

### 9.6 Reglas de negocio

1. **Un proyecto solo puede tener UN informe por fecha** (constraint unique en BD)
2. **Solo actividades con `Activo = true`** se cargan en nuevos informes
3. **Estados del informe:**
   - `Borrador` → se puede editar libremente
   - `EnRevision` → solo lectura para el creador
   - `Aprobado` → bloqueado, no se edita (crear nueva versión si es necesario)
   - `Rechazado` → vuelve a Borrador con observaciones
   - `Anulado` → registro histórico, no afecta cálculos

4. **Alertas automáticas se generan cuando:**
   - Desviación < -5%
   - Actividad crítica atrasada
   - Actividad sin reporte en 2 días
   - Restricción vencida
   - Avance > 100% (inconsistencia)
   - Variación > 30% vs día anterior

### 9.7 Decisiones arquitectónicas pendientes

#### ❓ Pregunta 1: ¿Cómo transformar el Excel en el sistema?

**El Excel actual tiene múltiples hojas:**
- Hojas tipo "BD" (bases de datos diarias por actividad)
- Hojas tipo "Curva S" (cálculos y gráficas)
- Hojas tipo "Resumen" (consolidación)
- Hoja "Informe Diario" (reporte final)

**Transformación propuesta:**

```
┌─────────────────────────────────────────────┐
│         EXCEL → SISTEMA WEB                 │
├─────────────────────────────────────────────┤
│ Hojas "BD"     → Tabla RegistroAvanceDiario │
│ Fórmulas Excel → Servicios C# (CalculoAvanceService) │
│ Hojas "Curva S" → Componente gráfico (Chart.js/ApexCharts) │
│ Hojas "Resumen" → Dashboard (componentes Blazor) │
│ Informe final  → Vista/Reporte web (exportable a PDF) │
└─────────────────────────────────────────────┘
```

#### ❓ Pregunta 2: ¿Cuándo calcular los indicadores?

**Opción A: On-demand** (recomendada para la mayoría)
- Los cálculos se ejecutan cuando el usuario abre un dashboard/reporte
- Siempre actualizado
- Puede ser lento si hay muchos datos (optimizable con índices)

**Opción B: Calcular y guardar**
- Los cálculos se guardan como campos en la BD
- Más rápido al consultar
- Requiere recalcular si hay cambios

**Decisión tomada:** Híbrido
- Campos básicos (AvanceEsperado, Desviación) → se calculan y guardan al crear registro
- Indicadores agregados (SPI, Curva S) → se calculan on-demand con cache en memoria

#### ❓ Pregunta 3: ¿Cómo estructurar la captura diaria?

**Opción elegida:** Un informe diario agrupa todos los registros de avance del día.

```
InformeDiario (fecha: 08/06/2026)
├─ RegistroAvanceDiario → Actividad: Hincado (52% avance)
│  ├─ Personal: Juan (8h), Carlos (8h), María (6h)
│  ├─ Equipos: Grúa GR-001 (7h), Excavadora EX-002 (6h)
│  ├─ Restricciones: RES-045 (Falta material)
│  └─ Fotografías: 3 fotos
├─ RegistroAvanceDiario → Actividad: Montaje estructuras (60% avance)
│  └─ ...
└─ Registro clima, observaciones generales, fotos generales
```

### 9.8 Pendientes antes de implementar

**⚠️ INFORMACIÓN FALTANTE DEL EXCEL:**

Para diseñar correctamente el modelo de datos y el flujo, necesitamos saber:

1. **¿Cuántas hojas tipo "BD" tiene el Excel?** (¿una por actividad? ¿cuántas actividades?)
2. **¿Qué hojas tipo "Curva S" existen?** (¿una por disciplina? ¿por frente de trabajo?)
3. **¿Qué hojas tipo "Resumen" hay?**
4. **¿La hoja "Informe Diario" es la que se imprime/exporta al final?**
5. **¿Hay otras hojas adicionales?** (clima, personal, equipos, restricciones como hojas separadas o están integradas)

**Una vez tengamos esta información, podemos:**
- Confirmar el modelo de datos definitivo
- Crear las migraciones
- Diseñar las pantallas exactas
- Implementar la lógica de cálculo

### 9.9 Migraciones necesarias (preliminar)

Cuando se apruebe el diseño final, se creará la migración:

```powershell
dotnet ef migrations add AgregarCamposModuloInformeDiario --project RenergeIA.Infrastructure --startup-project RenergeIA.Web
```

**Cambios estimados:**
- 6 tablas modificadas (23 columnas nuevas)
- 3 tablas nuevas (M:N)
- 2 enums nuevos
- Índices para optimización de consultas
- Constraint único: `ProyectoId + Fecha` en InformesDiarios

### 9.10 Estimación de esfuerzo

| Fase | Descripción | Tiempo estimado |
|---|---|---|
| Fase 1 | Modelo de datos (entidades, enums, migración) | 2-3 horas |
| Fase 2 | Capa Infrastructure (DbContext, repositorios) | 1-2 horas |
| Fase 3 | UI básica (lista, crear, ver informe) | 4-6 horas |
| Fase 4 | Lógica de negocio (cálculos, alertas, estados) | 3-4 horas |
| Fase 5 | Dashboard e indicadores (Curva S, KPIs) | 2-3 horas |
| Fase 6 | Reportes y exportación | 2-3 horas |
| **Total** | | **14-21 horas** |

### 9.11 Decisiones finales confirmadas (08/06/2026)

**Disciplinas del proyecto EPC (9 en total):**

1. ✅ Inicio de Contrato
2. ✅ Estudios de Ingeniería
3. ✅ Ingeniería de Detalle
4. ✅ Suministro
5. ✅ Construcción Civil
6. ✅ Mecánica
7. ✅ Eléctrica
8. ✅ Pruebas
9. ✅ Cierre de Contrato

**Enum creado:** `RenergeIA.Core/Enums/Disciplina.cs`

**Campos agregados:**
- `ActividadWBS.Disciplina` (tipo: `Disciplina`)
- `RegistroClima.InformeDiarioId` (FK opcional a InformeDiario)

**Migración aplicada:** `20260608161501_AgregarDisciplinaYRelacionClima`

**Plantilla WBS:** 110 actividades clasificadas por disciplina

**Curvas S a generar:**
- 1 Curva S General (todo el proyecto)
- 9 Curvas S por Disciplina (análisis individual)

### 9.12 Próximos pasos de implementación

- [x] Análisis funcional completado
- [x] Diagnóstico de estado actual
- [x] Identificación de campos faltantes
- [x] Diseño de flujo funcional
- [x] Definición de reglas de negocio
- [x] Propuesta de cálculos automáticos
- [x] Información del Excel obtenida y analizada
- [x] Modelo de datos confirmado con 9 disciplinas
- [x] Disciplina agregada a ActividadWBS
- [x] Plantilla de 110 actividades clasificada
- [ ] ⏳ **EN PROGRESO:** Implementación del módulo Informe Diario

### Checklist Etapa 9 (en implementación)

- [x] Análisis y diseño arquitectónico documentado
- [x] Flujo funcional definido
- [x] Campos faltantes identificados
- [x] Decisiones arquitectónicas evaluadas
- [x] Información del Excel obtenida
- [x] Modelo de datos aprobado
- [x] Enum Disciplina creado (9 valores)
- [x] ActividadWBS actualizado con Disciplina
- [x] RegistroClima relacionado con InformeDiario
- [x] Migración de disciplinas aplicada
- [ ] ⏳ **Implementación en progreso...**

### 9.13 Implementación del modelo de datos completada (08/06/2026)

**Enums creados:**
- `EstadoInforme` → Borrador, EnRevision, Aprobado, Rechazado, Anulado
- `EstadoAvance` → AlDia, Atrasado, Adelantado

**Entidades actualizadas:**
- `InformeDiario` → +7 campos (Estado, Version, InformeDiarioAnteriorId, RevisadoPor, FechaRevision, MotivoRechazo, ComentariosGenerales)
- `RegistroAvanceDiario` → +8 campos (CantidadEjecutadaDia, AvanceEsperado, AvanceAcumulado, Desviacion, DiasAtraso, Estado, HorasAfectadasClima, Novedades)

**Entidades M:N creadas:**
- `RegistroAvancePersonal` → Vincula personal que trabajó en la actividad
- `RegistroAvanceEquipo` → Vincula equipos utilizados
- `RegistroAvanceRestriccion` → Vincula restricciones que afectaron el avance

**DbContext configurado:**
- Tipos decimal con precisión/escala especificados
- Relación auto-referenciada en InformeDiario (versionado)
- Relaciones M:N con DeleteBehavior.Restrict para evitar cascadas

**Migración aplicada:**
- `20260608171109_ModuloInformeDiario`
- 3 tablas nuevas creadas
- Índices y foreign keys configurados correctamente

### 9.14 Implementación completa del módulo UI y servicios (08/06/2026)

**Servicio de cálculos creado: `InformeDiarioService.cs`**

Métodos implementados (12 en total):
1. `CalcularAvanceEsperado()` - Calcula el avance que debería tener según la programación inicial
2. `CalcularAvanceAcumuladoAsync()` - Suma todos los registros de avance hasta una fecha
3. `CalcularDesviacion()` - Diferencia entre avance real y esperado (negativo = atraso)
4. `CalcularSPI()` - Schedule Performance Index (< 1 = atraso, = 1 = a tiempo, > 1 = adelanto)
5. `DeterminarEstadoAvance()` - Clasifica como AlDia/Atrasado/Adelantado según desviación
6. `CalcularDiasAtraso()` - Estima días de atraso basado en el ritmo de avance
7. `ActualizarCalculosRegistroAvanceAsync()` - Actualiza TODOS los cálculos automáticamente
8. `ObtenerResumenPorDisciplinaAsync()` - Genera resumen de avance por cada disciplina
9. `GenerarCurvaSAsync()` - Genera datos para gráfica de Curva S (general o por disciplina)

**Características del servicio:**
- Cálculos completamente automáticos al guardar
- Fórmulas basadas en metodología de gestión de proyectos (SPI, desviación, etc.)
- Soporte para análisis por disciplina (9 disciplinas configuradas)
- Preparado para generar Curva S (falta solo la UI de gráfica)

**Páginas Blazor creadas (3 páginas):**

1. **ListaInformesDiarios.razor** (`/informes-diarios/{ProyectoId}`)
   - Lista paginada de todos los informes del proyecto
   - Filtrado por estado (Borrador, En Revisión, Aprobado, etc.)
   - Estados visuales con badges de colores
   - Acciones: Ver, Editar (solo borradores), Eliminar (solo borradores)
   - Confirmación antes de eliminar con advertencia
   - Eliminación en cascada de registros de avance asociados

2. **CrearInformeDiario.razor** (`/informes-diarios/{ProyectoId}/crear` y `/editar/{InformeId}`)
   - Formulario completo para crear/editar informes
   - Información general: fecha, certificado, personal, resumen, observaciones
   - Registro dinámico de avances por actividad
   - Agregar/eliminar actividades en tiempo real
   - Dropdown con todas las actividades del WBS
   - Validación de certificado único (no permite duplicados)
   - Cálculos automáticos al guardar (llama a InformeDiarioService)
   - Mensajes de error claros y descriptivos

3. **DetalleInformeDiario.razor** (`/informes-diarios/{ProyectoId}/{InformeId}`)
   - Vista completa y detallada del informe
   - Resumen ejecutivo por disciplina (9 disciplinas con iconos)
   - Tabla completa de avances con todos los cálculos
   - Visualización de desviaciones con badges de colores
   - Barras de progreso por disciplina
   - Indicadores de SPI (Schedule Performance Index)
   - Estados visuales (Al día en verde, Atrasado en rojo, Adelantado en azul)

**Mejoras de diseño y UX implementadas:**

1. **Nombres de disciplinas legibles:**
   - Método helper `ObtenerNombreDisciplina()` convierte enums a español
   - "InicioContrato" → "Inicio de Contrato"
   - "EstudiosIngenieria" → "Estudios de Ingeniería"
   - "ConstruccionCivil" → "Construcción Civil"
   - Aplicado en todas las páginas del módulo

2. **Iconos por disciplina:**
   - Método helper `ObtenerIconoDisciplina()` asigna emojis
   - 📝 Inicio de Contrato
   - 🔬 Estudios de Ingeniería
   - 📐 Ingeniería de Detalle
   - 📦 Suministro
   - 🏗️ Construcción Civil
   - ⚙️ Mecánica
   - ⚡ Eléctrica
   - 🔍 Pruebas
   - ✅ Cierre de Contrato

3. **Resumen por disciplina mejorado:**
   - Cards visuales con iconos y nombres
   - Barras de progreso con colores (verde = bien, rojo = atrasado)
   - Indicadores de SPI destacados
   - Layout limpio y profesional

**Validaciones implementadas:**

1. **Certificado único por proyecto:**
   - Validación en backend antes de guardar
   - Verifica que no exista otro informe con el mismo certificado
   - Mensaje de error claro: "Ya existe un informe con el número de certificado..."
   - Permite editar certificados sin conflicto (excluye el propio informe en edición)

2. **Seguridad por estados:**
   - Solo informes en estado "Borrador" pueden editarse
   - Solo informes en estado "Borrador" pueden eliminarse
   - Informes "En Revisión", "Aprobado", "Rechazado" o "Anulado" son de solo lectura
   - Protección de integridad de registros oficiales

3. **Eliminación segura:**
   - Confirmación obligatoria antes de eliminar
   - Muestra datos del informe a eliminar (certificado, fecha)
   - Advertencia de que la acción no se puede deshacer
   - Eliminación en cascada de registros asociados

**Integración con el sistema:**

- Servicio registrado en `Program.cs` con DI (Dependency Injection)
- Módulo activado en `DetalleProyecto.razor` (card "📋 Informe Diario")
- Navegación integrada con el resto del sistema
- Breadcrumbs y botones de "Volver" en todas las páginas
- Consistencia visual con el diseño existente (Bootstrap)

**Archivos creados/modificados:**

Archivos nuevos:
- `RenergeIA.Web/Services/InformeDiarioService.cs` (servicio de cálculos)
- `RenergeIA.Web/Components/Pages/InformeDiario/ListaInformesDiarios.razor`
- `RenergeIA.Web/Components/Pages/InformeDiario/CrearInformeDiario.razor`
- `RenergeIA.Web/Components/Pages/InformeDiario/DetalleInformeDiario.razor`

Archivos modificados:
- `RenergeIA.Web/Program.cs` (registro del servicio)
- `RenergeIA.Web/Components/Pages/Proyectos/DetalleProyecto.razor` (activación del módulo)

**Estado de la Etapa 9:**

- [x] Análisis funcional completado
- [x] Modelo de datos diseñado e implementado
- [x] Migraciones creadas y aplicadas
- [x] Servicio de cálculos implementado (12 métodos)
- [x] Interfaz de usuario completa (3 páginas)
- [x] Validaciones de negocio implementadas
- [x] Mejoras de diseño y UX aplicadas
- [x] Integración con el sistema completada
- [x] **Módulo funcional y probado**
- [ ] Curva S visual (método existe, falta gráfica)
- [ ] Dashboard de proyecto (KPIs)
- [ ] Exportación a PDF
- [ ] Vincular personal/equipos/restricciones (UI pendiente, tablas listas)

**Próximos pasos opcionales:**

1. Implementar gráfica de Curva S (método ya existe en el servicio)
2. Dashboard del proyecto con KPIs principales
3. Exportación de informes a PDF
4. UI para vincular personal específico a cada avance (tabla M:N ya existe)
5. UI para vincular equipos utilizados (tabla M:N ya existe)
6. UI para vincular restricciones (tabla M:N ya existe)
7. Sistema de alertas automáticas por atrasos

### 9.15 Control de versiones de informes - ✅ COMPLETADO (09/06/2026)

**Requisito:** Sistema de versionado alfabético para informes diarios con trazabilidad completa.

**Flujo de versiones implementado (Opción A):**
```
Informe CERT-2026-001:
├─ v0.a (Borrador) ← Primera versión
├─ v0.b (En Revisión) ← Con cambios/correcciones
├─ v0.c (En Revisión) ← Más ajustes
└─ v1.0 (Aprobado) ⭐ ← Versión OFICIAL
```

**Implementación:**

1. **Modelo de datos actualizado:**
   - Campo `Version` cambiado de `int` a `string` (para formato "0.a", "0.b", etc.)
   - Campo `ComentarioCambio` agregado (documenta qué cambió en cada versión)
   - Migración `20260609175401_ControlVersionesInformeDiario` aplicada

2. **Servicio de versionado (InformeDiarioService.cs):**
   - `SiguienteVersion()` → Incrementa versión (0.a → 0.b → 0.z → 0.aa)
   - `ConvertirAVersionAprobada()` → Convierte a oficial (0.c → 1.0)
   - `EsVersionAprobada()` → Verifica formato X.0

3. **Lógica de versiones automáticas:**
   - Al editar un informe, crea NUEVA versión (no sobrescribe)
   - Copia datos del anterior + incrementa versión
   - Vincula mediante `InformeDiarioAnteriorId`
   - Campo "¿Qué cambió?" en formulario

4. **Página de Historial (3 vistas):**
   - **Línea de Tiempo:** Vista vertical con íconos de estado
   - **Tabs:** Pestañas navegables entre versiones
   - **Dropdown:** Selector con vista de detalle
   - Acceso desde botón "Ver Historial" en detalle del informe

**Archivos:**
- `RenergeIA.Core/Entities/InformeDiario.cs`
- `RenergeIA.Web/Services/InformeDiarioService.cs`
- `RenergeIA.Web/Components/Pages/InformeDiario/CrearInformeDiario.razor`
- `RenergeIA.Web/Components/Pages/InformeDiario/DetalleInformeDiario.razor`
- `RenergeIA.Web/Components/Pages/InformeDiario/HistorialVersiones.razor` (nuevo)

5. **Lista mejorada (ListaInformesDiarios.razor):**
   - Muestra SOLO la última versión de cada certificado
   - Filtra automáticamente versiones antiguas

6. **Protección de eliminación:**
   - NO permite eliminar informes con historial
   - Solo se pueden eliminar informes sin versiones (0.a sin ediciones)

7. **Comparación de versiones (CompararVersiones.razor) - NUEVO:**
   - Selectores para 2 versiones
   - Vista lado a lado
   - Comparación de campos básicos + actividades
   - Actividades agregadas/eliminadas/modificadas con colores

8. **Estilos personalizados (informe-diario-styles.css) - NUEVO:**
   - CSS inspirado en Solar Admin
   - Colores RenergeIA
   - Animaciones y transiciones
   - **Pendiente de aplicar**

**Bugs corregidos:**
- ✅ Versiones se sobrescribían (fix: AsNoTracking + Detached)
- ✅ Actividades no se copiaban (fix: resetear IDs a 0)
- ✅ Validación bloqueaba ediciones (fix: validar solo al crear)
- ✅ Lista mostraba todas las versiones (fix: filtrar últimas)

**Estado:** ✅ COMPLETAMENTE FUNCIONAL

---

## ETAPA 6 — Módulo Personal: Mejoras Pendientes

### Mejora 1: Plantilla de personal recomendada según capacidad del proyecto

**Objetivo:** Sugerir cantidad y roles de personal según el tamaño del proyecto (kWp/MW).

**Recomendaciones por capacidad:**
| Capacidad | Personal Recomendado | Observaciones |
|---|---|---|
| < 5 MW | 15-20 personas | Proyecto pequeño |
| 5-10 MW | 25-30 personas | Proyecto mediano |
| 10-15 MW | 35-40 personas | Proyecto grande |
| 15-20 MW | 45-50 personas | Proyecto muy grande |
| > 20 MW | 60+ personas | Mega proyecto |

**Dónde mostrar:**
- Banner informativo en el módulo de Personal
- Sugerencia al crear nuevo proyecto
- Tooltip o ayuda contextual

**Estado:** ⏳ Pendiente - Requiere validación de números reales con el equipo

### Mejora 2: Importación masiva de personal desde Excel/CSV

**Objetivo:** Permitir cargar múltiples personas a la vez desde archivo.

**Campos requeridos en la plantilla:**
1. Nombre (obligatorio)
2. Cargo (obligatorio)
3. Documento (obligatorio, único)
4. Teléfono (opcional)
5. Email (obligatorio, único, validar formato)
6. Especialidad (obligatorio)
7. Otro/Observaciones (opcional)

**Validaciones a implementar:**
- Formato correcto del archivo (Excel .xlsx o CSV)
- Todos los campos obligatorios presentes
- Documento y Email únicos (no duplicados en BD)
- Formato de email válido
- Si alguna fila falla validación, mostrar errores específicos
- No guardar parcialmente (todo o nada)

**Flujo propuesto:**
1. Botón "📤 Importar Personal" en lista de personal
2. Descargar plantilla de ejemplo
3. Cargar archivo
4. Validación en backend
5. Mostrar preview de datos a importar
6. Confirmar e importar
7. Resumen: X personas importadas, Y errores

**Estado:** ⏳ Pendiente - Requiere definir plantilla Excel final

---

## Sistema de Roles y Permisos — Pendiente de Definición

### Matriz de permisos propuesta para Informe Diario

| Rol | Crear | Editar Borrador | Enviar a Revisión | Aprobar | Rechazar | Anular | Ver |
|---|---|---|---|---|---|---|---|
| Residente de Obra | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ | ✅ |
| Ingeniero Supervisor | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ | ✅ |
| Gerente de Proyecto | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Admin | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |

**Implementación requerida:**
- Crear usuarios de prueba (Residente, Supervisor)
- Aplicar restricciones en controladores/páginas
- Botones condicionales según rol
- Validación en backend (no solo frontend)
- Auditoría de quién hizo qué acción

**Estado:** ⏳ Pendiente - Requiere validación de roles y flujos con el equipo

---

## Cómo correr el proyecto en cualquier momento

```powershell
cd "C:\Users\Ing. Kevin\Desktop\PROYECTO AGENTE"
dotnet run --project RenergeIA.Web/RenergeIA.Web.csproj
```
Abrir: `http://localhost:5288`  
Detener: `Ctrl + C`
