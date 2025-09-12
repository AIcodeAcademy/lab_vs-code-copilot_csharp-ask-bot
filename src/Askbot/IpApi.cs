namespace AskBot
{
    /// <summary>
    /// Representa la respuesta de la API de geolocalización por IP.
    /// </summary>
    public record IpApi(
    string Query,
    string Status,
    string Country,
    string CountryCode,
    string Region,
    string RegionName,
    string City,
    string Zip,
    double Lat,
    double Lon,
    string Timezone,
    string Isp,
    string Org,
    string As
  );
}