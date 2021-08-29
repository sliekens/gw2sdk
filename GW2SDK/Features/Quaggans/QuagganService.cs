using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Quaggans.Http;
using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public sealed class QuagganService
    {
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly IQuagganReader quagganReader;

        public QuagganService(
            HttpClient http,
            IQuagganReader quagganReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.quagganReader = quagganReader ?? throw new ArgumentNullException(nameof(quagganReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<QuagganRef>> GetQuaggans()
        {
            var request = new QuaggansRequest();
            return await http.GetResourcesSet(request,
                    json => quagganReader.Quaggan.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetQuaggansIndex()
        {
            var request = new QuaggansIndexRequest();
            return await http.GetResourcesSet(request, json => quagganReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<QuagganRef>> GetQuagganById(string quagganId)
        {
            var request = new QuagganByIdRequest(quagganId);
            return await http.GetResource(request, json => quagganReader.Quaggan.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<QuagganRef>> GetQuaggansByIds(IReadOnlyCollection<string> quagganIds)
        {
            var request = new QuaggansByIdsRequest(quagganIds);
            return await http.GetResourcesSet(request,
                    json => quagganReader.Quaggan.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<QuagganRef>> GetQuaggansByPage(int pageIndex, int? pageSize = default)
        {
            var request = new QuaggansByPageRequest(pageIndex, pageSize);
            return await http.GetResourcesPage(request,
                    json => quagganReader.Quaggan.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
