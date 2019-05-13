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
            var json = await _http.GetStringAsync("/v2/build");
            return JsonConvert.DeserializeObject<Build>(json);
        }
    }
}
