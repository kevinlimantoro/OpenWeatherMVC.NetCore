using System;
using System.Net.Http;
using WeatherCities.Interfaces;
using WeatherCities.Models;

namespace UnitTest
{
    internal class HttpMockHandler
    {
        private readonly IOpenWeatherMap _weather;
        public HttpMockHandler(DependencyResolverHelper helper)
        {
            _weather = helper.GetService<IOpenWeatherMap>();
        }

        public OpenWeatherMap GetWeather(string city)
        {
            try
            {
                return _weather.Get(city);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException.GetType() == typeof(HttpRequestException))
                    return new OpenWeatherMap();
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
