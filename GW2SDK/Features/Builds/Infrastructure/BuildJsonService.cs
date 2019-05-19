using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Builds.Infrastructure
{
    public sealed class BuildJsonService : IBuildJsonService
    {
        private readonly HttpClient _http;

        public BuildJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetBuild()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/build"
            };

            return await _http.GetStringWithMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }
    }
}
