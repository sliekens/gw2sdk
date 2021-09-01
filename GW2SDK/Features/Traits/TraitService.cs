using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Traits.Http;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed class TraitService
    {
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly ITraitReader traitReader;

        public TraitService(
            HttpClient http,
            ITraitReader traitReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.traitReader = traitReader ?? throw new ArgumentNullException(nameof(traitReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Trait>> GetTraits(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitsRequest(language);
            return await http.GetResourcesSet(request, json => traitReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetTraitsIndex(CancellationToken cancellationToken = default)
        {
            var request = new TraitsIndexRequest();
            return await http.GetResourcesSet(request, json => traitReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Trait>> GetTraitById(
            int traitId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitByIdRequest(traitId, language);
            return await http.GetResource(request, json => traitReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Trait>> GetTraitsByIds(
#if NET
            IReadOnlySet<int> traitIds,
#else
            IReadOnlyCollection<int> traitIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitsByIdsRequest(traitIds, language);
            return await http.GetResourcesSet(request, json => traitReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Trait>> GetTraitsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request, json => traitReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
