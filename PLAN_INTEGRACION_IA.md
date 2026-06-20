# 🤖 Plan de Integración de IA con Claude API (Anthropic)

**Fecha:** Junio 20, 2026  
**Desarrollador:** Kevin Becerra  
**API:** Claude API by Anthropic

---

## 📋 Índice

1. [Introducción](#introducción)
2. [¿Qué es Claude API?](#qué-es-claude-api)
3. [Casos de Uso en RenergeIA](#casos-de-uso-en-renergeia)
4. [Arquitectura de Integración](#arquitectura-de-integración)
5. [Plan de Implementación](#plan-de-implementación)
6. [Costos y Consideraciones](#costos-y-consideraciones)
7. [Próximos Pasos](#próximos-pasos)

---

## 🎯 Introducción

Este documento describe el plan para integrar **Claude API de Anthropic** en RenergeIA, agregando capacidades de inteligencia artificial para análisis, predicción y asistencia en la gestión de proyectos EPC fotovoltaicos.

### Objetivos de la Integración:

1. **Análisis Predictivo**: Predecir atrasos y sobrecostos basado en históricos
2. **Asistente Inteligente**: ChatBot para consultas sobre el proyecto
3. **Generación de Reportes**: Creación automática de análisis narrativos
4. **Detección de Riesgos**: Identificación temprana de problemas
5. **Recomendaciones**: Sugerencias para optimización de recursos

---

## 🧠 ¿Qué es Claude API?

**Claude** es un modelo de lenguaje de gran escala (LLM) desarrollado por **Anthropic**, diseñado para:
- Conversación natural y asistencia
- Análisis de documentos y datos
- Generación de contenido
- Razonamiento complejo
- Seguridad y confiabilidad

### Modelos Disponibles:

| Modelo | Descripción | Tokens | Uso Recomendado |
|--------|-------------|--------|-----------------|
| **Claude Opus** | Más potente | 200K | Análisis complejos, razonamiento |
| **Claude Sonnet** | Balanceado | 200K | Uso general, mejor costo/beneficio |
| **Claude Haiku** | Rápido y económico | 200K | Consultas simples, respuestas rápidas |

### Ventajas sobre otras APIs:

- ✅ Ventana de contexto grande (200K tokens)
- ✅ Mejor comprensión de contexto largo
- ✅ Seguridad y alineación (no tóxico)
- ✅ Excelente para análisis de datos estructurados
- ✅ API simple y documentada

---

## 💡 Casos de Uso en RenergeIA

### 1. Asistente de Proyecto (ChatBot)

**Funcionalidad:**
Un asistente IA que responde preguntas sobre el proyecto actual.

**Ejemplos de consultas:**
- "¿Cuál es el avance real vs programado?"
- "¿Qué actividades están atrasadas?"
- "Resumen del proyecto hasta la fecha"
- "¿Cuáles son las restricciones críticas abiertas?"

**Implementación:**
```csharp
public class ClaudeService
{
    public async Task<string> ConsultarAsistenteAsync(int proyectoId, string pregunta)
    {
        // 1. Obtener contexto del proyecto (WBS, costos, restricciones, etc.)
        var contexto = await ObtenerContextoProyectoAsync(proyectoId);
        
        // 2. Construir prompt para Claude
        var prompt = $@"
        Eres un asistente experto en gestión de proyectos EPC fotovoltaicos.
        
        Contexto del proyecto:
        {contexto}
        
        Pregunta del usuario: {pregunta}
        
        Responde de forma clara y concisa, usando datos del contexto.
        ";
        
        // 3. Llamar a Claude API
        var respuesta = await _claudeClient.Messages.CreateAsync(new MessageRequest
        {
            Model = "claude-sonnet-4-20250514",
            MaxTokens = 1000,
            Messages = new[] { new Message { Role = "user", Content = prompt } }
        });
        
        return respuesta.Content[0].Text;
    }
}
```

---

### 2. Análisis Predictivo de Atrasos

**Funcionalidad:**
Predecir qué actividades tienen riesgo de atrasarse basándose en:
- Histórico de avances
- Clima
- Restricciones actuales
- Disponibilidad de recursos

**Prompt Example:**
```
Analiza los siguientes datos de actividades del proyecto y predice 
cuáles tienen mayor riesgo de atrasarse en los próximos 7 días:

Actividades:
- Hincado de estructuras: 65% avance, programado 75%, restricción clima abierta
- Instalación eléctrica: 82% avance, programado 80%, sin restricciones
- Obra civil: 45% avance, programado 60%, falta personal

Considera:
- Tendencia histórica de avance
- Impacto de restricciones
- Brecha actual vs programado

Responde en JSON con: { "actividad", "riesgo", "probabilidad", "recomendacion" }
```

**Output esperado:**
```json
{
  "actividades_en_riesgo": [
    {
      "actividad": "Hincado de estructuras",
      "riesgo": "ALTO",
      "probabilidad": "75%",
      "razon": "Atraso de 10% + restricción climática activa",
      "recomendacion": "Aumentar cuadrilla cuando clima mejore, considerar horario extendido"
    },
    {
      "actividad": "Obra civil",
      "riesgo": "CRÍTICO",
      "probabilidad": "90%",
      "razon": "Atraso de 15% + falta de personal",
      "recomendacion": "Incorporar personal adicional urgentemente, revisar subcontrato"
    }
  ]
}
```

---

### 3. Generación Automática de Análisis de Informes

**Funcionalidad:**
Generar sección "Análisis y Observaciones" del Informe Diario automáticamente.

**Input:**
- Actividades del día
- Avances registrados
- Restricciones
- Clima
- Personal/Equipos utilizados

**Output:**
Texto narrativo profesional estilo informe ejecutivo.

**Ejemplo:**
```
Durante la jornada del 20 de junio se observó un avance satisfactorio 
en las actividades de instalación eléctrica, alcanzando un 82% de ejecución, 
superando en 2 puntos porcentuales lo programado. 

Sin embargo, se identificó un atraso crítico en obras civiles (45% vs 60% 
programado), atribuible principalmente a la disponibilidad limitada de 
personal especializado. Se recomienda...
```

---

### 4. Detección de Anomalías en Costos

**Funcionalidad:**
Identificar partidas con desviaciones inusuales de costo.

**Análisis:**
- Comparar costo real vs presupuestado
- Detectar patrones anormales
- Alertar sobre sobrecostos significativos

**Prompt:**
```
Analiza las siguientes partidas y detecta anomalías en costos:

Partidas:
- Paneles solares: Presup $9B, Ejecutado $9.2B (102%)
- Inversores: Presup $1.8B, Ejecutado $2.5B (139%)  ⚠️
- Estructuras: Presup $360M, Ejecutado $340M (94%)

Identifica cuáles tienen desviación > 10% y sugiere posibles causas.
```

---

### 5. Optimización de Recursos (Histogramas)

**Funcionalidad:**
Sugerir redistribución de personal/equipos para optimizar costos.

**Análisis:**
```
Basado en el histograma actual de personal:
- Mes 5: 54 personas (pico)
- Mes 6: 22 personas (valle)

Sugiere redistribución para nivelar carga y reducir costos de 
movilización/desmovilización.
```

---

## 🏗️ Arquitectura de Integración

### Estructura de Capas:

```
┌─────────────────────────────────────────┐
│     RenergeIA.Web (Blazor Pages)        │
│   - Chat.razor (Asistente)              │
│   - Dashboard.razor (Análisis IA)       │
└─────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────┐
│    RenergeIA.Web.Services               │
│   - ClaudeService                       │
│   - AnalisisPredictivo Service          │
│   - ReporteIAService                    │
└─────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────┐
│    Anthropic.SDK (NuGet)                │
│   - Messages API                        │
│   - Streaming                           │
└─────────────────────────────────────────┘
                 ↓
┌─────────────────────────────────────────┐
│    Claude API (Anthropic)               │
│    https://api.anthropic.com            │
└─────────────────────────────────────────┘
```

### Flujo de Datos:

```
Usuario → Pregunta en Chat
    ↓
ClaudeService → Obtener contexto del proyecto (DB)
    ↓
ClaudeService → Construir prompt con contexto
    ↓
Claude API → Procesar y generar respuesta
    ↓
Usuario ← Respuesta formateada
```

---

## 🔧 Plan de Implementación

### Fase 1: Configuración Inicial (1 día)

**Paso 1: Obtener API Key de Anthropic**
1. Crear cuenta en https://console.anthropic.com
2. Obtener API Key
3. Configurar en `appsettings.json`:
```json
{
  "Anthropic": {
    "ApiKey": "sk-ant-...",
    "Model": "claude-sonnet-4-20250514",
    "MaxTokens": 4096
  }
}
```

**Paso 2: Instalar SDK**
```bash
dotnet add package Anthropic.SDK --version 1.0.0
```

**Paso 3: Crear servicio base**
```csharp
public class ClaudeService
{
    private readonly IAnthropicClient _client;
    
    public ClaudeService(IConfiguration config)
    {
        _client = new AnthropicClient(config["Anthropic:ApiKey"]);
    }
}
```

---

### Fase 2: Asistente de Proyecto (2 días)

**Componentes:**
1. `ClaudeService.cs` - Servicio de integración
2. `Chat.razor` - Interfaz de chat
3. `ChatMessage.cs` - Modelo de mensajes

**Features:**
- Chat interactivo estilo WhatsApp
- Historial de conversación
- Contexto del proyecto automático
- Respuestas en tiempo real

---

### Fase 3: Análisis Predictivo (3 días)

**Componentes:**
1. `AnalisisPredictivo Service` - Lógica de predicción
2. Widget en Dashboard - Mostrar predicciones
3. Sistema de alertas - Notificar riesgos

**Algoritmo:**
```csharp
public async Task<List<PrediccionActividad>> PredecirAtrasosAsync(int proyectoId)
{
    // 1. Obtener todas las actividades con % avance
    var actividades = await ObtenerActividadesConAvanceAsync(proyectoId);
    
    // 2. Construir datos para análisis
    var datos = actividades.Select(a => new {
        a.Nombre,
        AvanceReal = a.AvanceReal,
        AvanceProgramado = a.AvancePlanificado,
        Brecha = a.AvancePlanificado - a.AvanceReal,
        TieneRestricciones = a.RestriccionesActivas.Any()
    });
    
    // 3. Pedir a Claude que analice
    var prompt = $"Analiza y predice riesgos: {JsonSerializer.Serialize(datos)}";
    var response = await _claude.PreguntarAsync(prompt);
    
    // 4. Parsear respuesta JSON
    return JsonSerializer.Deserialize<List<PrediccionActividad>>(response);
}
```

---

### Fase 4: Generación de Reportes (2 días)

**Componentes:**
- Botón "Generar Análisis IA" en Informe Diario
- Claude genera texto narrativo basado en datos
- Usuario puede editar antes de guardar

---

## 💰 Costos y Consideraciones

### Pricing de Claude API (Junio 2026):

| Modelo | Input (por 1M tokens) | Output (por 1M tokens) |
|--------|----------------------|------------------------|
| Claude Opus | $15 | $75 |
| Claude Sonnet | $3 | $15 |
| Claude Haiku | $0.25 | $1.25 |

### Estimación de Uso:

**Caso: Asistente de Proyecto**
- Consultas por día: ~50
- Tokens promedio por consulta: 2,000 (input) + 500 (output)
- Costo mensual (Sonnet): ~$5.625 USD

**Caso: Análisis Predictivo**
- Ejecuciones por día: 2 (mañana y tarde)
- Tokens promedio: 5,000 (input) + 1,000 (output)
- Costo mensual (Sonnet): ~$4.50 USD

**Total estimado mensual: ~$10-15 USD** (muy económico)

### Recomendaciones:

1. **Usar Sonnet** para la mayoría de casos (mejor balance)
2. **Usar Haiku** para respuestas simples y rápidas
3. **Usar Opus** solo para análisis críticos complejos
4. **Cachear contexto** cuando sea posible para reducir tokens
5. **Limitar tokens de respuesta** al mínimo necesario

---

## 🚀 Próximos Pasos

### Inmediato (Esta semana):

1. ✅ Crear este documento de planificación
2. ⏳ Obtener API Key de Anthropic
3. ⏳ Instalar Anthropic.SDK
4. ⏳ Crear ClaudeService básico
5. ⏳ Probar conexión con API

### Corto Plazo (Próximas 2 semanas):

1. Implementar Chat Asistente
2. Widget de predicciones en Dashboard
3. Generación de análisis para Informe Diario

### Mediano Plazo (Próximo mes):

1. Detección de anomalías en costos
2. Optimización de histogramas
3. Recomendaciones automáticas por módulo

---

## 📚 Referencias

- **Anthropic Docs**: https://docs.anthropic.com
- **Claude API Reference**: https://docs.anthropic.com/claude/reference
- **Anthropic SDK .NET**: https://github.com/anthropics/anthropic-sdk-dotnet
- **Pricing**: https://www.anthropic.com/pricing

---

**Autor:** Kevin Becerra  
**Última actualización:** Junio 20, 2026  
**Estado:** 📋 Planificación completa - Listo para implementar
