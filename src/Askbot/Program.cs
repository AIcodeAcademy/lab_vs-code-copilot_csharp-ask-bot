using System;

namespace AskBot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("# AskBot here, welcome!");

                IpApi ip = null;
                try
                {
                    ip = IpApiClient.FetchIp();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: no se pudo obtener la IP: {ex.Message}");
                }

                if (ip != null)
                {
                    // Mostrar información básica de la IP si el tipo implementa ToString de forma útil.
                    Console.WriteLine($"Detected IP info: {ip}");
                    IpApiClient.ReportLocation(ip);
                }
                // Procesar argumentos de línea de comandos.
                if (args.Length > 0)
                {
                    // El primer argumento es el comando.
                    var cmd = args[0]?.Trim();
                    if (string.Equals(cmd, "weather", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("## Fetching weather information...");
                        try
                        {
                            OpenWeather weather = Weather.FetchWeather();
                            Weather.ReportWeather(weather);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching weather: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Comando no reconocido.4
                        PrintHelpMessage();
                    }
                }
                else
                {
                    // Sin argumentos, mostrar mensaje de ayuda.
                    PrintHelpMessage();
                }

                Console.WriteLine("Bye!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(intercept: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(intercept: true);
            }
        }

        private static void PrintHelpMessage()
        {
            Console.WriteLine("## Available commands:");
            Console.WriteLine("  - `weather` :  Fetch the current weather information");
        }
    }
}

