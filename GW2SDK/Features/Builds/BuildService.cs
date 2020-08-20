using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Builds.Impl;

namespace GW2SDK.Builds
{
    [PublicAPI]
    public sealed class BuildService
    {
        private readonly HttpClient _http;

        public BuildService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<Build?> GetBuild()
        {
            var request = new BuildRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            return BuildJsonReader.Instance.Read(jsonDocument);
        }
    }
}
