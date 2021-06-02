using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuthWeatherApp.Data
{
    public static class ParseExtensions
    {
        public static RootParsed ToParsedRoot(this Root root)
        {
            return new RootParsed
            {
                Provinces = root.provincias.Select(province => new Province
                {
                    Name = province.NOMBRE_PROVINCIA,
                    Cities = root.ciudades?.Where(ciudad => ciudad.idProvince == province.CODPROV)?.Select(ciudad =>ciudad.ToCity())
                })
            };
        }
        public static RootParsed ToParsedRootFromSingleProvincia(this RootSingleProvincia root)
        {
            return new RootParsed
            {
                Provinces = Enumerable.Range(0,1).Select(province => new Province
                {
                    Name = root?.provincia?.NOMBRE_PROVINCIA,                    
                    Cities = root?.ciudades?.Where(ciudad => ciudad.idProvince == root?.provincia?.CODPROV)?.Select(ciudad => ciudad.ToCity())
                })
            };
        }
        public static City ToCity(this Ciudad ciudad)
        {
            return new City
            {
                Name = ciudad.name,
                Temperatures = ciudad.temperatures
            };
        }
        public static OpenWeatherCity ToOpenWeatherCity(this RootOpenWeather root)
        {
            return new OpenWeatherCity
            {
                Name = root?.name,
                Temperatures = new TemperaturesFeelsLike
                {
                    min = root?.main?.temp_min.ToCelsiusFromKelvin().ToString(),
                    max = root?.main?.temp_max.ToCelsiusFromKelvin().ToString(),
                    feelsLike = root?.main?.feels_like.ToCelsiusFromKelvin().ToString()
                }
            };
        }
        public static double ToCelsiusFromKelvin(this double kelvins)
        {
            return kelvins - 273.16;
        }
    }
}
