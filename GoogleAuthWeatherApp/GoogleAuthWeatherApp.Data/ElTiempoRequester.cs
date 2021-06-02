using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuthWeatherApp.Data
{
    public class ElTiempoRequester : IElTiempoRequester
    {
        private HttpClient _httpClient { get; set; } = new HttpClient();
        /// <inheritdoc/>
        public async Task<RootParsed> GetElTiempoResultsParsed()
        {
            try
            {
                var request = await _httpClient.GetAsync("https://www.el-tiempo.net/api/json/v2/home");
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    ConcurrentDictionary<RootParsed, string> intermediateResults = new ConcurrentDictionary<RootParsed, string>();
                    List<Task> requests = new List<Task>();
                    var provinces = JsonConvert.DeserializeObject<Root>(response).provincias.Select(provincia => provincia.CODPROV).Distinct().ToArray();
                    Parallel.ForEach(provinces, id => requests.Add(ActionForId(id, intermediateResults)));
                    await Task.WhenAll(requests.ToArray());
                    return new RootParsed
                    {
                        Provinces = intermediateResults.SelectMany(ir => ir.Key.Provinces).Distinct().OrderBy(pr => pr.Name)
                    };
                }
                throw new Exception("Server response status is " + request.StatusCode);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message ?? e.InnerException?.Message);
                return default;
            }

        }
        private Task ActionForId(string id, ConcurrentDictionary<RootParsed, string> intermediateResults)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var requestPerProvince = await _httpClient.GetAsync("https://www.el-tiempo.net/api/json/v2/provincias/" + id);
                    if (requestPerProvince.IsSuccessStatusCode)
                    {
                        var responsePerProvince = await requestPerProvince.Content.ReadAsStringAsync();
                        var root = JsonConvert.DeserializeObject<RootSingleProvincia>(responsePerProvince);
                        intermediateResults.TryAdd(root.ToParsedRootFromSingleProvincia(), string.Empty);
                    }
                    throw new Exception("Server response status is " + requestPerProvince.StatusCode);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message ?? e.InnerException?.Message);
                }
            });
        }
    }
    public interface IElTiempoRequester
    {
        /// <summary>
        /// Get data from all Spain provinces at once
        /// </summary>
        /// <returns><see cref="Task"/>&lt;<see cref="RootParsed"/>&gt;</returns>
        Task<RootParsed> GetElTiempoResultsParsed();
    }
}
