// Plan (pseudocódigo detallado):
// 1. Definir el record principal `OpenWeather` que represente la respuesta JSON completa.
// 2. Añadir propiedades mapeadas con `JsonPropertyName`:
//    - latitude (double)
//    - longitude (double)
//    - generationtime_ms (double)
//    - utc_offset_seconds (int)
//    - timezone (string)
//    - timezone_abbreviation (string)
//    - elevation (double)
//    - current_weather_units (objeto) -> `CurrentWeatherUnits`
//    - current_weather (objeto) -> `CurrentWeather`
// 3. Definir `CurrentWeatherUnits` con propiedades string para cada campo del objeto:
//    - time, interval, temperature, windspeed, winddirection, is_day, weathercode
//    Mapear cada una con `JsonPropertyName` exactamente igual que el JSON.
// 4. Definir `CurrentWeather` con tipos adecuados:
//    - time (string), interval (int), temperature (double),
//      windspeed (double), winddirection (int), is_day (int), weathercode (int)
//    Mapear cada propiedad con `JsonPropertyName`.
// 5. Colocar todo en el namespace `AskBot` y añadir `using System.Text.Json.Serialization`.
// 6. Mantener la forma de `record` inmutable para deserialización con `System.Text.Json`.
// 7. Incluir comentarios y nombres idénticos a los campos JSON para claridad y compatibilidad.

using System.Text.Json.Serialization;

namespace AskBot
{
    public record OpenWeather(
        [property: JsonPropertyName("latitude")] double Latitude,
        [property: JsonPropertyName("longitude")] double Longitude,
        [property: JsonPropertyName("generationtime_ms")] double GenerationtimeMs,
        [property: JsonPropertyName("utc_offset_seconds")] int UtcOffsetSeconds,
        [property: JsonPropertyName("timezone")] string Timezone,
        [property: JsonPropertyName("timezone_abbreviation")] string TimezoneAbbreviation,
        [property: JsonPropertyName("elevation")] double Elevation,
        [property: JsonPropertyName("current_weather_units")] CurrentWeatherUnits CurrentWeatherUnits,
        [property: JsonPropertyName("current_weather")] CurrentWeather CurrentWeather
    );

    public record CurrentWeatherUnits(
        [property: JsonPropertyName("time")] string Time,
        [property: JsonPropertyName("interval")] string Interval,
        [property: JsonPropertyName("temperature")] string Temperature,
        [property: JsonPropertyName("windspeed")] string Windspeed,
        [property: JsonPropertyName("winddirection")] string Winddirection,
        [property: JsonPropertyName("is_day")] string IsDay,
        [property: JsonPropertyName("weathercode")] string Weathercode
    );

    public record CurrentWeather(
        [property: JsonPropertyName("time")] string Time,
        [property: JsonPropertyName("interval")] int Interval,
        [property: JsonPropertyName("temperature")] double Temperature,
        [property: JsonPropertyName("windspeed")] double Windspeed,
        [property: JsonPropertyName("winddirection")] int Winddirection,
        [property: JsonPropertyName("is_day")] int IsDay,
        [property: JsonPropertyName("weathercode")] int Weathercode
    );
}