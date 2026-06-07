# RenergeIA — Plataforma EPC Fotovoltaico

Plataforma web interna para gestión de proyectos EPC fotovoltaicos de Renergeia S.A.S.  
Desarrollada con Blazor Server + ASP.NET Core + SQL Server + Entity Framework Core.

---

## Estado actual: Etapa 1 COMPLETADA ✓

---

## Entorno instalado

| Herramienta | Versión / Detalle |
|---|---|
| Sistema operativo | Windows 11 |
| .NET SDK | 10.0.300 |
| SQL Server | 2025 Developer Edition — instancia DEFAULT (`MSSQLSERVER`) |
| SSMS | Instalado — conectar con `localhost` (NO usar `localhost\SQLEXPRESS`) |
| Git | 2.54.0.windows.1 |
| VS Code | Instalado |

> **IMPORTANTE:** La edición instalada es Developer, no Express. Por eso la cadena de conexión usa `localhost` y no `localhost\SQLEXPRESS`.

---

## Cómo correr el proyecto

```powershell
# Desde la raíz: C:\Users\Ing. Kevin\Desktop\PROYECTO AGENTE
dotnet run --project RenergeIA.Web/RenergeIA.Web.csproj
```

Luego abrir en el navegador: `http://localhost:5288`

Para detener: `Ctrl + C` en la terminal.

---

## Estructura del proyecto

```
PROYECTO AGENTE/
├── RenergeIA.slnx                    ← Solución (.NET 10 usa .slnx, no .sln)
├── RenergeIA.Web/                    ← Blazor Server — capa de presentación
├── RenergeIA.Core/                   ← Entidades de dominio e interfaces
└── RenergeIA.Infrastructure/         ← EF Core, repositorios, acceso a datos
```

### Referencias entre proyectos
- `RenergeIA.Web` → depende de `RenergeIA.Core` y `RenergeIA.Infrastructure`
- `RenergeIA.Infrastructure` → depende de `RenergeIA.Core`
- `RenergeIA.Core` → no depende de nadie (capa base)

---

## Decisiones técnicas importantes

- `.NET 10` en lugar de `.NET 8 LTS` — todos los comandos usan `--framework net10.0`
- La plantilla de Blazor en .NET 10 es `dotnet new blazor --interactivity Server` (no `blazorserver`, que fue eliminado)
- El archivo de solución es `.slnx` (nuevo formato de .NET 10), no `.sln`
- Para agregar proyectos a la solución usar `dotnet sln add` sin especificar el nombre del archivo (lo detecta solo)
- Git inicializado en la raíz del proyecto

---

## Fases del proyecto

| Fase | Meses | Descripción |
|---|---|---|
| Fase 1 | 1–4 | Operativa: login, proyectos, WBS, avance diario, costos, documentos, fotografías, restricciones, informe diario, alertas |
| Fase 2 | 5–8 | Analítica: dashboard gerencial, IA (OpenAI), reportes automáticos, SharePoint, WhatsApp API, clima |
| Fase 3 | 9–14 | Avanzada: HSE completo, compras, QA/QC, predicción por históricos |

---

## Módulos y arquitectura

- **18 módulos** definidos en la documentación funcional (`RenergeIA_Documentacion_v1.0.pdf`)
- **13 roles RBAC** (control de acceso por rol)
- **15 reglas de negocio** (RN-01 a RN-15)
- **20 alertas** automáticas (A-01 a A-20)
- **Entidad central:** `Proyecto` — todo el modelo de datos gira alrededor de ella

---

## Próxima etapa: Etapa 2 — Modelo de Datos

Crear las entidades C# en `RenergeIA.Core/Entities/` según la documentación.

Entidades a crear (secciones 19 y 20 del PDF):

```
Proyecto, Usuario, ActividadWBS, RegistroAvanceDiario, InformeDiario,
Fotografia, Documento, VersionDocumento, Partida, CostoReal,
NoConformidad, AccionCorrectiva, Restriccion, PersonalProyecto,
DocumentoPersona, Equipo, RegistroHorometro, Mantenimiento,
Alerta, RegistroClima
```

Luego: instalar paquetes NuGet (EF Core, Identity, SQL Server provider) y crear el `DbContext`.

---

## Checklist Etapa 1

- [x] .NET SDK instalado y verificado (`dotnet --version`)
- [x] SQL Server 2025 Developer Edition instalado
- [x] SSMS instalado y conectado a `localhost`
- [x] Git instalado y configurado (user: Kevin Becerra)
- [x] VS Code instalado
- [x] Solución `RenergeIA.slnx` creada
- [x] Proyecto `RenergeIA.Web` (Blazor) creado con .NET 10
- [x] Proyecto `RenergeIA.Core` (Class Library) creado con .NET 10
- [x] Proyecto `RenergeIA.Infrastructure` (Class Library) creado con .NET 10
- [x] Proyectos agregados a la solución
- [x] Referencias entre proyectos configuradas
- [x] Git inicializado (`git init`)
- [x] `dotnet build` — compilación exitosa (3 proyectos en 5.8s)
- [x] `dotnet run` — aplicación corriendo en `http://localhost:5288`

---

## Documentación fuente

Archivo: `RenergeIA_Documentacion_v1.0.pdf` (33 páginas, Junio 2026)  
Toda decisión técnica y funcional debe respetar este documento.
