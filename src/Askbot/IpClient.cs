using System;
using System.Net.Http;
using System.Text.Json;

namespace AskBot
{
    /// <summary>
    /// Cliente simple para consultar la API pública de geolocalización por IP (http://ip-api.com/).
    /// </summary>
    /// <remarks>
    /// Esta clase proporciona un método sincrónico de conveniencia <see cref="FetchIp"/> para
    /// obtener información sobre la dirección IP pública del host que ejecuta la aplicación.
    /// Utiliza un <see cref="HttpClient"/> compartido y opciones de <see cref="JsonSerializer"/>.
    /// </remarks>
    public class IpApiClient
    {
        /// <summary>
        /// <see cref="HttpClient"/> compartido para reutilización de conexiones.
        /// </summary>
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Opciones para el serializador JSON: usa nomenclatura camelCase y formato indentado.
        /// </summary>
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        /// <summary>
        /// Consulta la API externa para obtener información de la IP pública y la deserializa en <see cref="IpApi"/>.
        /// </summary>
        /// <returns>
        /// Una instancia de <see cref="IpApi"/> con los datos devueltos por la API en caso de éxito;
        /// <c>null</c> si ocurre cualquier excepción durante la llamada, lectura o deserialización.
        /// </returns>
        public static IpApi FetchIp()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync("http://ip-api.com/json/").Result;
                string statusCode = response.StatusCode.ToString();
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Error HTTP: {statusCode}");
                }
                string json = response.Content.ReadAsStringAsync().Result;
                IpApi ipApi = JsonSerializer.Deserialize<IpApi>(json, _jsonOptions);
                return ipApi;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Presenta por consola la información de localización contenida en el <see cref="IpApi"/>.
        /// </summary>
        /// <param name="ipApi">Instancia de <see cref="IpApi"/> a reportar.</param>
        public static void ReportLocation(IpApi ipApi)
        {
            if (ipApi == null)
            {
                Console.WriteLine("> No hay datos de IP para mostrar.");
                return;
            }

            Console.WriteLine($"## Your IP address is: {ipApi.Query}");
            string location = $"{ipApi.City}, {ipApi.RegionName}, {ipApi.Country}";
            Console.WriteLine($"- Location: {location}");
            string coordinates = $"Lat {ipApi.Lat}, Long {ipApi.Lon}";
            Console.WriteLine($"- Coordinates: {coordinates}");
        }
    }
}
