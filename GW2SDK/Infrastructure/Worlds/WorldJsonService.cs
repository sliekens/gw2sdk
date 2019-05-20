using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Worlds;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class WorldJsonService : IWorldJsonService
    {
        private readonly HttpClient _http;

        public WorldJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<HttpResponseMessage> GetWorldIds()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds"
            };

            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetWorldById(int worldId)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = $"id={worldId}"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetWorldsById([NotNull] IReadOnlyList<int> worldIds)
        {
            if (worldIds == null) throw new ArgumentNullException(nameof(worldIds));
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = $"ids={worldIds.ToCsv()}"
            };

            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetAllWorlds()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = "ids=all"
            };

            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetWorldsByPage(int page, int? pageSize)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = $"page={page}"
            };

            if (pageSize.HasValue)
            {
                resource.Query += $"&page_size={pageSize}";
            }

            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }
    }
}
