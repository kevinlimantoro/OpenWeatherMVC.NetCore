using WeatherCities.Models;

namespace WeatherCities.Interfaces
{
    public interface IOpenWeatherMap
    {
        OpenWeatherMap Get(string city);
    }
}
