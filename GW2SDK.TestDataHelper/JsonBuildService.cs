using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.TestDataHelper
{
    public class JsonBuildService
    {
        private readonly HttpClient _http;

        public JsonBuildService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetJsonBuild(bool indented)
        {
            using (var request = new GetBuildRequest())
            using (var response = await _http.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JToken.Parse(json).ToString(indented ? Formatting.Indented : Formatting.None);
            }
        }
    }
}
