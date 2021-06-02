using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuthWeatherApp.Data
{
    #region ElTiempoRequesterModels
    public class RootParsed
    {
        public IEnumerable<Province> Provinces { get; set; }
    }
    public class Province
    {
        public string Name { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
    public class City
    {
        public string Name { get; set; }
        public Temperatures Temperatures { get; set; }
        public string Description => $"max {Temperatures.max} ºC / min {Temperatures.min} ºC";
    }
    #endregion
    #region OpenWeatherMapRequesterModels
    public class TemperaturesFeelsLike : Temperatures
    {
        public string feelsLike { get; set; }
    }
    public class OpenWeatherCity
    {
        public string Name { get; set; }
        public TemperaturesFeelsLike Temperatures { get; set; }
        public string Description => $"max {Temperatures.max} ºC / min {Temperatures.min} ºC, feels like {Temperatures.feelsLike} ºC";
    }
    #endregion
}
