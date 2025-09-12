using System;
using System.Net.Http;
using System.Text.Json;

namespace AskBot
{
    /// <summary>
    /// Cliente simple para consultar la API p�blica de geolocalizaci�n por IP (http://ip-api.com/).
    /// </summary>
    /// <remarks>
    /// Esta clase proporciona un m�todo sincr�nico de conveniencia <see cref="FetchIp"/> para
    /// obtener informaci�n sobre la direcci�n IP p�blica del host que ejecuta la aplicaci�n.
    /// Utiliza un <see cref="HttpClient"/> compartido y opciones de <see cref="JsonSerializer"/>.
    /// </remarks>
    public class IpApiClient
    {
        /// <summary>
        /// <see cref="HttpClient"/> compartido para reutilizaci�n de conexiones.
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
        /// Consulta la API externa para obtener informaci�n de la IP p�blica y la deserializa en <see cref="IpApi"/>.
        /// </summary>
        /// <returns>
        /// Una instancia de <see cref="IpApi"/> con los datos devueltos por la API en caso de �xito;
        /// <c>null</c> si ocurre cualquier excepci�n durante la llamada, lectura o deserializaci�n.
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
        /// Presenta por consola la informaci�n de localizaci�n contenida en el <see cref="IpApi"/>.
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
