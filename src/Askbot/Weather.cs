using System;
using System.Net.Http;
using System.Text.Json;
using System.Globalization;

namespace AskBot
{
    public class Weather
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// Consulta la API de open-meteo.com con las coordenadas devueltas por <see cref="IpApiClient.FetchIp"/>.
        /// Devuelve la instancia deserializada o null en caso de error.
        /// </summary>
        public static OpenWeather FetchWeather()
        {
            try
            {
                var ipApi = IpApiClient.FetchIp();
                if (ipApi == null)
                {
                    return null;
                }

                double lat = ipApi.Lat;
                double lon = ipApi.Lon;

                string latStr = lat.ToString(CultureInfo.InvariantCulture);
                string lonStr = lon.ToString(CultureInfo.InvariantCulture);

                string url = $"https://api.open-meteo.com/v1/forecast?latitude={latStr}&longitude={lonStr}&current_weather=true&timezone=auto";
                HttpResponseMessage response = _httpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Error HTTP: {response.StatusCode}");
                }

                string json = response.Content.ReadAsStringAsync().Result;
                OpenWeather weather = JsonSerializer.Deserialize<OpenWeather>(json, _jsonOptions);
                return weather;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Presenta por consola la información de <see cref="OpenMeteoResponse.CurrentWeather"/>
        /// </summary>
        /// <param name="weather">Instancia de <see cref="OpenMeteoResponse"/> a reportar.</param>
        public static void ReportWeather(OpenWeather weather)
        {
            if (weather == null)
            {
                Console.WriteLine("> No hay datos meteorológicos para mostrar.");
                return;
            }
            CurrentWeather currentWeather = weather.CurrentWeather;
            Console.WriteLine("## Current weather");
            Console.WriteLine($"- Temperature: {currentWeather.Temperature} ");
        }
    }

}