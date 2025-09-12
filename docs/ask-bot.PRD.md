---
ID: 'DEMO_ASK-BOT'
Curso: 'Copilot-CSharp'
Arquetipo: 'CSharp-CLI'
Autor: 'Alberto Basalo'
---
# PRD — AskBot CLI

### 1. Resumen Ejecutivo

CLI educativo que consulta APIs públicas para dar información básica de la IP del usuario y servicios asociados: localización, clima, moneda, hora y sol. El objetivo es que los alumnos implementen un **cliente de APIs REST** funcional que demuestre el consumo de servicios externos y el manejo de datos JSON.

#### 1.1 Objetivos y Métricas

  * Desarrollar una aplicación CLI funcional con todos los **comandos** operativos de forma local.
  * Lograr que la aplicación sea ejecutable con el comando `dotnet run <command>`.
  * Demostrar el consumo exitoso de **APIs REST** públicas y gratuitas.
  * Implementar manejo de errores para servicios externos no disponibles.

#### 1.2 Audiencia

  * **Alumnos:** Para adquirir experiencia práctica en consumo de APIs.
  * **Instructores:** Para utilizar como guía y validar el aprendizaje.

### 2. Alcance

  * **Incluido:** CLI con 5 comandos principales para consultar información de geolocalización, clima, moneda, tiempo y datos solares.
  * **Excluido:** Interfaz gráfica, persistencia de datos, autenticación, notificaciones.

#### 2.1 Historias de Usuario

  * Como usuario, quiero saber mi localización actual a partir de mi IP.
  * Como usuario, quiero conocer el pronóstico de hoy en mi ciudad.
  * Como usuario, quiero ver la moneda oficial de mi país y cotizaciones básicas.
  * Como usuario, quiero conocer mi hora local y diferencia con UTC.
  * Como usuario, quiero saber las horas de salida y puesta del sol.

#### 2.2 Requisitos Funcionales

1.  **RF-1:** `loc` o `[vacío]` - Muestra ciudad, país, latitud/longitud obtenidos de [ipapi.co](https://ipapi.co/json/) o [ip-api.com](http://ip-api.com/json/).
2.  **RF-2:** `weather` - Muestra temperatura actual, probabilidad de lluvia y código meteorológico desde [open-meteo.com](https://open-meteo.com/).
3.  **RF-3:** `money` - Muestra moneda oficial del país y cotización en EUR/USD/GBP/CHF desde [open.er-api.com/v6](https://open.er-api.com/v6).
4.  **RF-4:** `time` - Muestra hora local, huso horario, horario de verano/invierno y diferencia con UTC desde [timeapi.io](https://timeapi.io/).
5.  **RF-5:** `sun` - Muestra horas de salida y puesta del sol desde [open-meteo.com](https://open-meteo.com/).

#### 2.3 Requisitos Funcionales Adicionales

6.  **RF-6:** `askbot help` - Muestra ayuda con la lista de comandos disponibles y su descripción.
7.  **RF-7:** `askbot --version` - Muestra la versión actual de la aplicación.
8.  **RF-8:** Todos los comandos deben manejar errores de conectividad y mostrar mensajes informativos.

#### 2.4 Requisitos No Funcionales

  * **Simplicidad:** Priorizar la claridad y la simplicidad en la implementación y uso.
  * **Rendimiento:** Las respuestas de la CLI deben ser rápidas, preferiblemente en menos de 1 segundo con conexión activa.
  * **Disponibilidad:** Manejar amigablemente la no disponibilidad de servicios externos.
  * **Usabilidad:** Comandos intuitivos y mensajes de error claros y útiles.
  * **Tecnologías:** Utilizar Dotnet 9+ y librerías NuGet mínimas.

-----

### 3. Modelo de Datos y APIs

#### 3.1 APIs Utilizadas

  * **Geolocalización:**
      * `ipapi.co/json/` - Información de ubicación basada en IP
      * `ip-api.com/json/` - API alternativa para geolocalización

  * **Clima:**
      * `open-meteo.com` - Datos meteorológicos actuales y predicciones

  * **Moneda:**
      * `open.er-api.com/v6` - Tasas de cambio de divisas

  * **Tiempo:**
      * `timeapi.io` - Información de zona horaria y tiempo

#### 3.2 Estructura de Respuestas Esperadas

  * **Comando `loc`:**
      * Ciudad, País
      * Latitud, Longitud
      * Código de país (ISO)

  * **Comando `weather`:**
      * Temperatura actual (°C)
      * Probabilidad de precipitación (%)
      * Descripción del clima

  * **Comando `money`:**
      * Moneda local (código y nombre)
      * Cotizaciones vs EUR, USD, GBP, CHF

  * **Comando `time`:**
      * Hora local actual
      * Zona horaria
      * Diferencia con UTC
      * Estado horario de verano

  * **Comando `sun`:**
      * Hora de salida del sol
      * Hora de puesta del sol
      * Duración del día

### 4. Criterios de Aceptación y Riesgos

#### 4.1 Criterios de Aceptación

  * Cada comando devuelve datos legibles en formato texto estructurado.
  * La CLI puede ser ejecutada con `dotnet run <command>` sin errores.
  * Se manejan adecuadamente los errores de conectividad y APIs no disponibles.
  * Los comandos responden en menos de 3 segundos en condiciones normales.
  * La aplicación muestra mensajes de ayuda claros cuando se ejecuta sin parámetros.

#### 4.2 Riesgos

  * **APIs externas no disponibles:** Implementar mensajes de fallback informativos.
  * **Límites de uso de APIs:** Informar al usuario sobre posibles restricciones.
  * **Conectividad de red:** Manejar timeouts y errores de conexión graciosamente.
  * **Cambios en APIs:** Las APIs públicas pueden cambiar su estructura sin previo aviso.

### 5. Datos de Ejemplo

#### 5.1 Ejemplo de Salida - Comando `loc`
```
📍 Ubicación:
   Ciudad: Madrid
   País: España (ES)
   Coordenadas: 40.4168, -3.7038
```

#### 5.2 Ejemplo de Salida - Comando `weather`
```
🌤️ Clima en Madrid:
   Temperatura: 22°C
   Precipitación: 15%
   Condición: Parcialmente nublado
```

#### 5.3 Ejemplo de Salida - Comando `money`
```
💰 Moneda: Euro (EUR)
   1 EUR = 1.00 EUR
   1 EUR = 1.08 USD
   1 EUR = 0.86 GBP
   1 EUR = 0.97 CHF
```

#### 5.4 Ejemplo de Salida - Comando `time`
```
🕐 Hora Local:
   Actual: 14:30:25
   Zona: Europe/Madrid (CET)
   UTC+2 (Horario de verano)
```

#### 5.5 Ejemplo de Salida - Comando `sun`
```
☀️ Info Solar:
   Amanecer: 07:45
   Atardecer: 19:20
   Duración del día: 11h 35m
```


