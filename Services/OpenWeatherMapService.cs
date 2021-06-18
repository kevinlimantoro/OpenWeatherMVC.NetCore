using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using WeatherCities.Interfaces;
using WeatherCities.Models;

namespace WeatherCities.Services
{
    public class OpenWeatherMapService : IOpenWeatherMap
    {
        private readonly IMemoryCache _cache;
        private readonly OpenWeatherMapConfig _config;
        private readonly IHttpClientFactory _client;
        private readonly string DB_KEY = "KEY_OPENWEATHER";
        public OpenWeatherMapService(IMemoryCache cache, IConfiguration configuration, IHttpClientFactory client)
        {
            _cache = cache;
            _config = configuration.GetSection("OpenWeather").Get<OpenWeatherMapConfig>();
            _config.APIEndpoint = _config.APIEndpoint.Replace("{API_KEY}", _config.APIKey);
            _client = client;
        }

        public OpenWeatherMap Get(string city)
        {
            return _cache.GetOrCreateAsync<OpenWeatherMap>(DB_KEY + city, async entry =>
              {
                  entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                  var response = default(OpenWeatherMap);
                  using (var client = _client.CreateClient())
                  {
                      var res = await client.GetAsync(_config.APIEndpoint.Replace("{CITY}", city));
                      if (res.IsSuccessStatusCode)
                      {
                          var responseStream = await res.Content.ReadAsStringAsync();
                          response = JsonConvert.DeserializeObject<OpenWeatherMap>(responseStream);
                      }
                  }
                  return response;
              }).Result;
        }
    }
}
