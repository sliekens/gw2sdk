using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.MailCarriers.Http;
using JetBrains.Annotations;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    public sealed class MailCarrierService
    {
        private readonly HttpClient http;

        private readonly IMailCarrierReader mailCarrierReader;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public MailCarrierService(
            HttpClient http,
            IMailCarrierReader mailCarrierReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.mailCarrierReader = mailCarrierReader ?? throw new ArgumentNullException(nameof(mailCarrierReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<MailCarrier>> GetMailCarriers(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MailCarriersRequest(language);
            return await http
                .GetResourcesSet(request, json => mailCarrierReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMailCarriersIndex(CancellationToken cancellationToken = default)
        {
            var request = new MailCarriersIndexRequest();
            return await http.GetResourcesSet(request,
                    json => mailCarrierReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<MailCarrier>> GetMailCarrierById(
            int mailCarrierId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MailCarrierByIdRequest(mailCarrierId, language);
            return await http.GetResource(request, json => mailCarrierReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MailCarrier>> GetMailCarriersByIds(
#if NET
            IReadOnlySet<int> mailCarrierIds,
#else
            IReadOnlyCollection<int> mailCarrierIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MailCarriersByIdsRequest(mailCarrierIds, language);
            return await http
                .GetResourcesSet(request, json => mailCarrierReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<MailCarrier>> GetMailCarriersByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MailCarriersByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => mailCarrierReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
