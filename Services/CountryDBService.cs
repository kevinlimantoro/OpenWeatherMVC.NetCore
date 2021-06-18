using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeatherCities.Interfaces;

namespace WeatherCities.Services
{
    public class CountryDBService : ICountryDB
    {
        private readonly IMemoryCache _cache;
        private readonly string DB_KEY = "KEY_JOBJECT";
        private readonly string DB_KEY_COUNTRY = "KEY_COUNTRY";
        private readonly string DB_KEY_CITY = "KEY_CITY";
        public CountryDBService(IMemoryCache cache)
        {
            _cache = cache;
        }

        private JObject GetRawJobject()
        {
            return _cache.GetOrCreate(DB_KEY, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                var raw = File.ReadAllText("countries.min.json");
                return JsonConvert.DeserializeObject<JObject>(raw);
            });
        }

        public List<string> GetCities(string country)
        {
            return _cache.GetOrCreate(DB_KEY_CITY + country, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                var countrydb = GetRawJobject();
                var arr = JArray.Parse(countrydb[country].ToString());
                return arr.Select(x => x.ToString()).ToList();
            });
        }

        public List<string> GetCountries()
        {
            return _cache.GetOrCreate(DB_KEY_COUNTRY, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                var countrydb = GetRawJobject();
                var res = countrydb.Properties().Select(x => x.Name);
                return res.ToList();
            });
        }
    }
}
