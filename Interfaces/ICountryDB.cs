using System.Collections.Generic;

namespace WeatherCities.Interfaces
{
    public interface ICountryDB
    {
        List<string> GetCountries();
        List<string> GetCities(string country);
    }
}
