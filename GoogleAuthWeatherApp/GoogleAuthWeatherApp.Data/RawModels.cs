using System;
using System.Collections.Generic;

namespace GoogleAuthWeatherApp.Data
{
    #region ElTiempoRequesterModels
    public class StateSky
    {
        public string description { get; set; }
        public string id { get; set; }
    }

    public class Temperatures
    {
        public string max { get; set; }
        public string min { get; set; }
    }

    public class Ciudad
    {
        public string id { get; set; }
        public string idProvince { get; set; }
        public string name { get; set; }
        public string nameProvince { get; set; }
        public StateSky stateSky { get; set; }
        public Temperatures temperatures { get; set; }

    }
    public class Comautonoma
    {
        public string ID { get; set; }
        public string CODAUTON { get; set; }
        public string CODCOMUN { get; set; }
        public string NOMBRE { get; set; }
    }
    public class Today
    {
        public List<string> p { get; set; }
    }

    public class Tomorrow
    {
        public List<string> p { get; set; }
    }
    public class TodaySingle
    {
        public string p { get; set; }
    }

    public class TomorrowSingle
    {
        public string p { get; set; }
    }
    public class Provincia
    {
        public string CODPROV { get; set; }
        public string NOMBRE_PROVINCIA { get; set; }
        public string CODAUTON { get; set; }
        public string COMUNIDAD_CIUDAD_AUTONOMA { get; set; }
        public string CAPITAL_PROVINCIA { get; set; }
    }

    public class Root
    {
        public string title { get; set; }
        public List<Ciudad> ciudades { get; set; }
        public Today today { get; set; }
        public Tomorrow tomorrow { get; set; }
        public List<Provincia> provincias { get; set; }
        public List<object> breadcrumb { get; set; }
        public string metadescripcion { get; set; }
        public string keywords { get; set; }
    }
    public class RootSingleProvincia
    {
        public Comautonoma comautonoma { get; set; }
        public Provincia provincia { get; set; }
        public string title { get; set; }
        public List<Ciudad> ciudades { get; set; }
        public TodaySingle today { get; set; }
        public TomorrowSingle tomorrow { get; set; }
        public List<object> breadcrumb { get; set; }
        public string metadescripcion { get; set; }
        public string keywords { get; set; }
    }
    #endregion

    #region OpenWeatherMapRequesterModels
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class RootOpenWeather
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }


    #endregion
}
