using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skins.Http;
using JetBrains.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    public sealed class SkinService
    {
        private readonly HttpClient _http;

        private readonly ISkinReader _skinReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public SkinService(HttpClient http, ISkinReader skinReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _skinReader = skinReader ?? throw new ArgumentNullException(nameof(skinReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetSkinsIndex()
        {
            var request = new SkinsIndexRequest();
            return await _http.GetResourcesSet(request, json => _skinReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Skin>> GetSkinById(int skinId)
        {
            var request = new SkinByIdRequest(skinId);
            return await _http.GetResource(request, json => _skinReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Skin>> GetSkinsByIds(IReadOnlyCollection<int> skinIds)
        {
            var request = new SkinsByIdsRequest(skinIds);
            return await _http.GetResourcesSet(request, json => _skinReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Skin>> GetSkinsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new SkinsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _skinReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
