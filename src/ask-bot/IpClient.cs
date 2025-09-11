using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AskBot
{
  public class IpApiClient
  {
    private static readonly HttpClient _httpClient = new HttpClient();

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
        var options = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        IpApi ipApi = JsonSerializer.Deserialize<IpApi>(json, options);
        Console.WriteLine($"Your IP address is: {ipApi?.Query}");
        string location = ipApi?.City + ", " + ipApi?.RegionName + ", " + ipApi?.Country;
        Console.WriteLine($"Location: {location}");
        string coordinates = "Lat " + ipApi?.Lat + ", Long " + ipApi?.Lon;
        Console.WriteLine($"Coordinates: {coordinates}");
        return ipApi;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Exception occurred: {ex.Message}");
        return null;
      }
    }
  }
}
