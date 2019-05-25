using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Builds;
using Newtonsoft.Json;

namespace GW2SDK.Features.Builds
{
    public sealed class BuildService
    {
        private readonly HttpClient _http;

        public BuildService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<Build> GetBuild([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetBuildRequest())
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Build>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }
    }
}
