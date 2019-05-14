using System.Net.Http;
using System.Threading.Tasks;

namespace GW2SDK.Builds.Infrastructure
{
    public class JsonBuildService : IJsonBuildService
    {
        private readonly HttpClient _http;

        public JsonBuildService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetBuildAsync()
        {
            return await _http.GetStringAsync("/v2/build");
        }
    }
}