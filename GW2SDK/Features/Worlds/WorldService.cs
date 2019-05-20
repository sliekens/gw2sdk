using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Common;
using Newtonsoft.Json;

namespace GW2SDK.Features.Worlds
{
    public sealed class WorldService
    {
        private readonly IWorldJsonService _api;

        public WorldService([NotNull] IWorldJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IDataTransferList<int>> GetWorldIds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetWorldIds().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<int>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list, listContext);
        }

        public async Task<World> GetWorldById(int worldId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetWorldById(worldId).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<World>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferList<World>> GetWorldsById([NotNull] IReadOnlyList<int> worldIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (worldIds == null) throw new ArgumentNullException(nameof(worldIds));
            var response = await _api.GetWorldsById(worldIds).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<World>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<World>(list, listContext);
        }

        public async Task<IDataTransferList<World>> GetAllWorlds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetAllWorlds().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<World>();
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<World>(list, listContext);
        }

        public async Task<IDataTransferPage<World>> GetWorldsByPage(int page, int? pageSize = null,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetWorldsByPage(page, pageSize).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<World>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<World>(list, pageContext);
        }
    }
}
