using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System.Linq;
using WeatherCities;
using WeatherCities.Interfaces;

namespace UnitTest
{
    public class Tests
    {
        private DependencyResolverHelper _serviceProvider;
        private ICountryDB _countryDB;
        private IOpenWeatherMap _weather;
        [SetUp]
        public void Setup()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
            _countryDB = _serviceProvider.GetService<ICountryDB>();
            _weather = _serviceProvider.GetService<IOpenWeatherMap>();
        }

        [Test]
        public void GetCountry()
        {
            var res = _countryDB.GetCountries();
            Assert.AreEqual(true, res.Any());
        }
        [Test]
        [TestCase("United Kingdom")]
        [TestCase("United States")]
        [TestCase("Indonesia")]
        [TestCase("Taiwan")]
        [TestCase("China")]
        public void GetCity(string country)
        {
            var res = _countryDB.GetCities(country);
            Assert.AreEqual(true, res.Any());
        }
        [Test]
        [TestCase("New Zealand")]
        [TestCase("Surabaya")]
        [TestCase("Jakarta")]
        [TestCase("Taipei")]
        [TestCase("Shanghai")]
        [TestCase("Singapore")]
        public void GetWeather(string city)
        {
            var res = _weather.Get(city);
            Assert.AreNotEqual(null, res);
        }
    }
}