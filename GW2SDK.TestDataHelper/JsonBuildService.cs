using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds.Http;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper
{
    public class JsonBuildService
    {
        private readonly HttpClient _http;

        public JsonBuildService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetJsonBuild()
        {
            var request = new BuildRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return json.RootElement.ToString() ?? throw new InvalidOperationException();
        }
    }
}
