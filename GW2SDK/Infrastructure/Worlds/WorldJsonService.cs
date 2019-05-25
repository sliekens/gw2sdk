using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
            using (var request = new GetWorldIdsRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetWorldById(int worldId)
        {
            using (var request = new GetWorldByIdRequest(worldId))
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetWorldsById([NotNull] IReadOnlyList<int> worldIds)
        {
            using (var request = new GetWorldsByIdRequest(worldIds))
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetAllWorlds()
        {
            using (var request = new GetAllWorldsRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetWorldsByPage(int page, int? pageSize)
        {
            using (var request = new GetWorldsByPageRequest(page, pageSize))
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }
    }
}
