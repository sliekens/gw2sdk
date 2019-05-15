using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;

namespace GW2SDK.Builds.Infrastructure
{
    public sealed class JsonBuildService : IJsonBuildService
    {
        private readonly HttpClient _http;

        public JsonBuildService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<string> GetBuildAsync()
        {
            return await _http.GetStringAsync("/v2/build");
        }
    }
}