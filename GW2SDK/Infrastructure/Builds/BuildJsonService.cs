using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Builds;

namespace GW2SDK.Infrastructure.Builds
{
    public sealed class BuildJsonService : IBuildJsonService
    {
        private readonly HttpClient _http;

        public BuildJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<HttpResponseMessage> GetBuild()
        {
            using (var request = new GetBuildRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }
    }
}
