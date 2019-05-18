using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Worlds.Infrastructure
{
    public sealed class JsonWorldService : IJsonWorldService
    {
        private readonly HttpClient _http;

        public JsonWorldService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<(string Json, ListMetaData MetaData)> GetAllWorlds()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = "ids=all"
            };

            return await _http.GetStringWithListMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<(string Json, ListMetaData MetaData)> GetWorldIds()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds"
            };

            return await _http.GetStringWithListMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<string> GetWorldById(int worldId)
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = $"id={worldId}"
            };
            return await _http.GetStringAsync(resource.Uri).ConfigureAwait(false);
        }

        public async Task<(string Json, ListMetaData MetaData)> GetWorldsById([NotNull] IReadOnlyList<int> worldIds)
        {
            if (worldIds == null) throw new ArgumentNullException(nameof(worldIds));
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/worlds",
                Query = $"ids={worldIds.ToCsv()}"
            };

            return await _http.GetStringWithListMetaDataAsync(resource.Uri).ConfigureAwait(false);
        }
    }
}
