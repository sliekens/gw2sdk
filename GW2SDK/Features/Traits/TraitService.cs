﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Traits.Http;
using GW2SDK.Traits.Json;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    public sealed class TraitService
    {
        private readonly HttpClient http;

        public TraitService(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<Trait>> GetTraits(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => TraitReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetTraitsIndex(CancellationToken cancellationToken = default)
        {
            var request = new TraitsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Trait>> GetTraitById(
            int traitId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitByIdRequest(traitId, language);
            return await http.GetResource(request,
                    json => TraitReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Trait>> GetTraitsByIds(
#if NET
            IReadOnlySet<int> traitIds,
#else
            IReadOnlyCollection<int> traitIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitsByIdsRequest(traitIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => TraitReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Trait>> GetTraitsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TraitsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => TraitReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
