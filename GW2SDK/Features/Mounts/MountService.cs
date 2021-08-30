using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Mounts.Http;
using JetBrains.Annotations;

namespace GW2SDK.Mounts
{
    [PublicAPI]
    public sealed class MountService
    {
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly IMountReader mountReader;

        public MountService(
            HttpClient http,
            IMountReader mountReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.mountReader = mountReader ?? throw new ArgumentNullException(nameof(mountReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Mount>> GetMounts(Language? language = default)
        {
            var request = new MountsRequest(language);
            return await http
                .GetResourcesSet(request, json => mountReader.Mount.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MountName>> GetMountNames()
        {
            var request = new MountNamesRequest();
            return await http.GetResourcesSet(request,
                    json => mountReader.MountId.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Mount>> GetMountByName(MountName mountName, Language? language = default)
        {
            var request = new MountByNameRequest(mountName, language);
            return await http.GetResource(request, json => mountReader.Mount.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Mount>> GetMountsByNames(
            IReadOnlyCollection<MountName> mountNames,
            Language? language = default
        )
        {
            var request = new MountsByNamesRequest(mountNames, language);
            return await http
                .GetResourcesSet(request, json => mountReader.Mount.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Mount>> GetMountsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new MountsByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => mountReader.Mount.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MountSkin>> GetMountSkins(Language? language = default)
        {
            var request = new MountSkinsRequest(language);
            return await http.GetResourcesSet(request,
                    json => mountReader.MountSkin.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMountSkinsIndex()
        {
            var request = new MountSkinsIndexRequest();
            return await http.GetResourcesSet(request,
                    json => mountReader.MountSkinId.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<MountSkin>> GetMountSkinById(int mountSkinId, Language? language = default)
        {
            var request = new MountSkinByIdRequest(mountSkinId, language);
            return await http.GetResource(request, json => mountReader.MountSkin.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MountSkin>> GetMountSkinsByIds(
            IReadOnlyCollection<int> mountSkinIds,
            Language? language = default
        )
        {
            var request = new MountSkinsByIdsRequest(mountSkinIds, language);
            return await http.GetResourcesSet(request,
                    json => mountReader.MountSkin.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<MountSkin>> GetMountSkinsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new MountSkinsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => mountReader.MountSkin.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
