using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skins.Http;
using GW2SDK.Skins.Json;
using JetBrains.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    public sealed class SkinService
    {
        private readonly HttpClient http;

        public SkinService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<int>> GetSkinsIndex(CancellationToken cancellationToken = default)
        {
            var request = new SkinsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Skin>> GetSkinById(
            int skinId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkinByIdRequest(skinId, language);
            return await http.GetResource(request,
                    json => SkinReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Skin>> GetSkinsByIds(
#if NET
            IReadOnlySet<int> skinIds,
#else
            IReadOnlyCollection<int> skinIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkinsByIdsRequest(skinIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => SkinReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Skin>> GetSkinsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new SkinsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => SkinReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
