using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Features.Common.Infrastructure;
using GW2SDK.Features.Worlds.Infrastructure;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Worlds
{
    public sealed class WorldService
    {
        private readonly IJsonWorldService _api;

        public WorldService([NotNull] IJsonWorldService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IDataTransferList<int>> GetWorldIds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var list = new List<int>();
            var (json, metaData) = await _api.GetWorldIds().ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list)
            {
                MetaData = metaData.GetListMetaData()
            };
        }

        public async Task<World> GetWorldById(int worldId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            var (json, _) = await _api.GetWorldById(worldId).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<World>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferList<World>> GetWorldsById([NotNull] IReadOnlyList<int> worldIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (worldIds == null) throw new ArgumentNullException(nameof(worldIds));
            var list = new List<World>();
            var (json, metaData) = await _api.GetWorldsById(worldIds).ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<World>(list)
            {
                MetaData = metaData.GetListMetaData()
            };
        }

        public async Task<IDataTransferList<World>> GetAllWorlds([CanBeNull] JsonSerializerSettings settings = null)
        {
            var list = new List<World>();
            var (json, metaData) = await _api.GetAllWorlds().ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<World>(list)
            {
                MetaData = metaData.GetListMetaData()
            };
        }

        public async Task<IDataTransferPagedList<World>> GetWorldsByPage(int page, int? pageSize = null,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            var list = new List<World>();
            var (json, metaData) = await _api.GetWorldsByPage(page, pageSize).ConfigureAwait(false);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPagedList<World>(list)
            {
                MetaData = metaData.GetPagedListMetaData()
            };
        }
    }
}
