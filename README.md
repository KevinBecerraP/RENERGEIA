# RenergeIA — Plataforma EPC Fotovoltaico

Plataforma web interna para gestión de proyectos EPC fotovoltaicos de Renergeia S.A.S.  
Desarrollada con Blazor Server + ASP.NET Core + SQL Server + Entity Framework Core.

---

## Estado actual: Etapas 1-9 COMPLETADAS ✅

**Módulos funcionando:**
- ✅ Login y autenticación (13 roles configurados)
- ✅ Proyectos (CRUD completo)
- ✅ Personal del Proyecto (CRUD completo con filtros)
- ✅ Equipos y Maquinaria (CRUD completo)
- ✅ WBS / Actividades (árbol jerárquico + plantilla 110 actividades EPC con 9 disciplinas)
- ✅ Informe Diario / Avance Diario (registro de avances, cálculos automáticos, resumen por disciplina)

**Funcionalidades del módulo Informe Diario:**
- 📊 Registro de avance diario por actividad
- 🔢 Cálculos automáticos (SPI, desviación, días de atraso)
- 📈 Resumen ejecutivo por disciplina (9 disciplinas)
- 🔒 Validación de certificados únicos
- 🗑️ Gestión de estados (Borrador, En Revisión, Aprobado)
- ✏️ Edición/eliminación solo en estado Borrador

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

## Próxima etapa: Etapa 9 — Módulo Informe Diario

**Objetivo:** Implementar el sistema de captura y seguimiento de avances diarios de actividades, con cálculos automáticos de desviaciones, alertas y generación de Curva S.

**Pendiente antes de implementar:**
- Análisis de las hojas del Excel del Informe Diario actual
- Confirmación del modelo de datos (23 campos nuevos + 3 tablas M:N identificados)
- Diseño de pantallas y flujos de usuario

**Documentación:**
- Especificaciones: `prom informe diario.txt`
- Análisis arquitectónico: `GUIA_DESARROLLO.md` > Etapa 9

---

## Checklist de Progreso

### ✅ Etapa 1: Preparación del Entorno
- [x] .NET SDK 10, SQL Server, SSMS, Git, VS Code instalados
- [x] Solución y 3 proyectos creados (Web, Core, Infrastructure)
- [x] Referencias configuradas y compilación exitosa

### ✅ Etapa 2: Modelo de Datos
- [x] 20 entidades creadas en `RenergeIA.Core/Entities`
- [x] 12 enums definidos en `RenergeIA.Core/Enums`
- [x] DbContext configurado con Entity Framework Core
- [x] Migración `InitialCreate` aplicada (19 tablas creadas)

### ✅ Etapa 3: Identity y Roles
- [x] ASP.NET Core Identity configurado
- [x] 13 roles definidos (Administrador, Gerente, Residente, etc.)
- [x] Usuario admin creado: `admin@renergeia.com` / `Admin123!`
- [x] Migración `AddIdentity` aplicada (7 tablas Identity)

### ✅ Etapa 4: Login UI
- [x] Página de login funcional (`/login`)
- [x] Logout (`/logout`)
- [x] Protección de rutas con `[Authorize]`
- [x] Redirección automática si no autenticado

### ✅ Etapa 5: Módulo Proyectos
- [x] CRUD completo (Listar, Crear, Ver, Editar, Eliminar)
- [x] Badge de colores por estado
- [x] Validaciones con DataAnnotations

### ✅ Etapa 6: Módulo Personal
- [x] CRUD completo vinculado a proyectos
- [x] Filtros por tipo, estado y búsqueda
- [x] Toggle activo/inactivo

### ✅ Etapa 7: Módulo Equipos
- [x] CRUD completo de maquinaria y equipos
- [x] 6 tipos de equipo con badges de color
- [x] Filtros y búsqueda

### ✅ Etapa 8: Módulo WBS / Actividades
- [x] Árbol jerárquico de actividades
- [x] Plantilla con 110 actividades tipo EPC fotovoltaico
- [x] Toggle activo/inactivo por actividad
- [x] Migración `AddActivoWBS` aplicada

### 📋 Etapa 9: Informe Diario (En Análisis)
- [x] Análisis arquitectónico completado
- [x] 23 campos nuevos identificados
- [x] Flujo funcional documentado
- [ ] Información del Excel pendiente
- [ ] Implementación pendiente

---

## Documentación

| Archivo | Descripción |
|---|---|
| `GUIA_DESARROLLO.md` | Documentación técnica completa de todas las etapas (1,306 líneas) |
| `INFORME_REVISION_COMPLETA.md` | Revisión exhaustiva del estado del proyecto (generado 08/06/2026) |
| `prom informe diario.txt` | Especificaciones del módulo Informe Diario (261 líneas) |
| `README.md` | Este archivo — guía rápida y estado actual |

**Documentación fuente original:**  
`RenergeIA_Documentacion_v1.0.pdf` (33 páginas, Junio 2026) — Especificación funcional completa del sistema.
