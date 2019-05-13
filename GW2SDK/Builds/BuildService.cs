using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Builds
{
    public class BuildService
    {
        private readonly HttpClient _http;

        public BuildService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Build> GetBuildAsync()
        {
            var response = await _http.GetAsync("/v2/build");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Build>(json);
        }
    }
}
