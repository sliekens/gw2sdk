using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Builds
{
    [PublicAPI]
    public sealed class BuildService
    {
        private readonly IBuildReader _buildReader;
        private readonly HttpClient _http;

        public BuildService(HttpClient http, IBuildReader buildReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _buildReader = buildReader ?? throw new ArgumentNullException(nameof(buildReader));
        }

        public async Task<Build> GetBuild()
        {
            var request = new BuildRequest();
            return await _http.GetResource(request, json => _buildReader.Read(json))
                .ConfigureAwait(false);
        }
    }
}
