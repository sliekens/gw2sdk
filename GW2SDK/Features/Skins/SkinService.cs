using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly ISkinReader skinReader;

        public SkinService(
            HttpClient http,
            ISkinReader skinReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.skinReader = skinReader ?? throw new ArgumentNullException(nameof(skinReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetSkinsIndex(CancellationToken cancellationToken = default)
        {
            var request = new SkinsIndexRequest();
            return await http.GetResourcesSet(request, json => skinReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Skin>> GetSkinById(
            int skinId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkinByIdRequest(skinId, language);
            return await http.GetResource(request, json => skinReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Skin>> GetSkinsByIds(
            IReadOnlyCollection<int> skinIds,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkinsByIdsRequest(skinIds, language);
            return await http.GetResourcesSet(request, json => skinReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Skin>> GetSkinsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkinsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => skinReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
