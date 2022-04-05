using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Quaggans.Http;
using GW2SDK.Quaggans.Json;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public sealed class QuagganService
    {
        private readonly HttpClient http;

        public QuagganService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<Quaggan>> GetQuaggans(
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new QuaggansRequest();
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => QuagganReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetQuaggansIndex(CancellationToken cancellationToken = default)
        {
            var request = new QuaggansIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Quaggan>> GetQuagganById(
            string quagganId,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new QuagganByIdRequest(quagganId);
            return await http.GetResource(request,
                    json => QuagganReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Quaggan>> GetQuaggansByIds(
#if NET
            IReadOnlySet<string> quagganIds,
#else
            IReadOnlyCollection<string> quagganIds,
#endif
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new QuaggansByIdsRequest(quagganIds);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => QuagganReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Quaggan>> GetQuaggansByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new QuaggansByPageRequest(pageIndex, pageSize);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => QuagganReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
