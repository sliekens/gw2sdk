using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;
using GW2SDK.Infrastructure.Worlds;
using Newtonsoft.Json;

namespace GW2SDK.Features.Worlds
{
    public sealed class WorldService
    {
        private readonly HttpClient _http;

        public WorldService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<int>> GetWorldIds([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldIdsRequest())
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<int>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<int>(list, listContext);
            }
        }

        public async Task<World> GetWorldById(int worldId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldByIdRequest(worldId))
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<World>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<World>> GetWorldsById([NotNull] IReadOnlyList<int> worldIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (worldIds == null) throw new ArgumentNullException(nameof(worldIds));
            using (var request = new GetWorldsByIdRequest(worldIds))
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<World>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<World>(list, listContext);
            }
        }

        public async Task<IDataTransferList<World>> GetAllWorlds([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAllWorldsRequest())
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<World>();
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<World>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<World>> GetWorldsByPage(int page, int? pageSize = null,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetWorldsByPageRequest(page, pageSize))
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<World>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<World>(list, pageContext);
            }
        }
    }
}
