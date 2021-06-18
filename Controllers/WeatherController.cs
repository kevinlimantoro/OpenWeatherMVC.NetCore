using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using WeatherCities.Interfaces;
using WeatherCities.Models;

namespace WeatherCities.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly ICountryDB _db;
        private readonly IOpenWeatherMap _weather;

        public WeatherController(ILogger<WeatherController> logger, ICountryDB countryDb, IOpenWeatherMap weather)
        {
            _logger = logger;
            _db = countryDb;
            _weather = weather;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Country()
        {
            return Ok(ResponseResult.Success<List<string>>(_db.GetCountries()));
        }
        public IActionResult City(string country)
        {
            return Ok(ResponseResult.Success<List<string>>(_db.GetCities(country)));
        }
        public IActionResult GetWeather(string city)
        {
            return Ok(ResponseResult.Success<OpenWeatherMap>(_weather.Get(city)));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
