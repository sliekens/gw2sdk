using System;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Builds.Http;
using GW2SDK.Http;

namespace GW2SDK.Builds
{
    [PublicAPI]
    public sealed class BuildService
    {
        private readonly HttpClient _http;

        private readonly IBuildReader _buildReader;

        public BuildService(HttpClient http, IBuildReader buildReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _buildReader = buildReader ?? throw new ArgumentNullException(nameof(buildReader));
        }

        public async Task<Build?> GetBuild()
        {
            var request = new BuildRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _buildReader.Read(json);
        }
    }
}
