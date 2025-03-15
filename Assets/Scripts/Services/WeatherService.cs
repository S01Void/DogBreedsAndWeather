using System.Threading.Tasks;
using Zenject;

public class WeatherService
{
    [Inject]
    private readonly HttpRequestHandler _httpHandler;

    public async Task<string> GetWeather()
    {
        string url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        return await _httpHandler.GetAsync(url);
    }
}