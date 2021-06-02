using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuthWeatherApp.Data
{
    public class OpenWeatherMapRequester : IOpenWeatherMapRequester
    {
        private readonly static string OpenWeatherMapApiKey = "273e54dfbc39274a1d995f65e4ef40a8";
        private readonly static string RequestBackBone = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid="+ OpenWeatherMapApiKey;

        private HttpClient _httpClient { get; set; } = new HttpClient();
        /// <inheritdoc/>
        public async Task<OpenWeatherCity> GetWeatherFromCoordinates(string lat, string lon)
        {
            try
            {
                var request = await _httpClient.GetAsync(string.Format(RequestBackBone, lat, lon));
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RootOpenWeather>(response).ToOpenWeatherCity();
                }
                throw new Exception("Server response status is " + request.StatusCode);
            }
            catch (Exception ex)
            {
                return default;
            }

        }
    }
    public interface IOpenWeatherMapRequester
    {
        /// <summary>
        /// Get weather data from latitude and longitude cootdinates
        /// </summary>
        /// <returns><see cref="Task"/>&lt;<see cref="OpenWeatherCity"/>&gt;</returns>
        Task<OpenWeatherCity> GetWeatherFromCoordinates(string lat, string lon);
    }
}
