using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Mounts.Http;
using GW2SDK.Mounts.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts
{
    [PublicAPI]
    public sealed class MountService
    {
        private readonly HttpClient http;

        public MountService(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

        #region /v2/mounts/types

        public async Task<IReplicaSet<Mount>> GetMounts(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => MountReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MountName>> GetMountNames(
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountNamesRequest();
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => MountNameReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Mount>> GetMountByName(
            MountName mountName,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountByNameRequest(mountName, language);
            return await http.GetResource(request,
                    json => MountReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Mount>> GetMountsByNames(
            IReadOnlyCollection<MountName> mountNames,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountsByNamesRequest(mountNames, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => MountReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Mount>> GetMountsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => MountReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion

        #region /v2/mounts/skins

        public async Task<IReplicaSet<MountSkin>> GetMountSkins(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountSkinsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => MountSkinReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMountSkinsIndex(CancellationToken cancellationToken = default)
        {
            var request = new MountSkinsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<MountSkin>> GetMountSkinById(
            int mountSkinId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountSkinByIdRequest(mountSkinId, language);
            return await http.GetResource(request,
                    json => MountSkinReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MountSkin>> GetMountSkinsByIds(
#if NET
            IReadOnlySet<int> mountSkinIds,
#else
            IReadOnlyCollection<int> mountSkinIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountSkinsByIdsRequest(mountSkinIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => MountSkinReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<MountSkin>> GetMountSkinsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MountSkinsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => MountSkinReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
