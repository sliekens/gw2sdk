using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly HttpClient _http;

        private readonly IMailCarrierReader _mailCarrierReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public MailCarrierService(
            HttpClient http,
            IMailCarrierReader mailCarrierReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _mailCarrierReader = mailCarrierReader ?? throw new ArgumentNullException(nameof(mailCarrierReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<MailCarrier>> GetMailCarriers(Language? language = default)
        {
            var request = new MailCarriersRequest(language);
            return await _http
                .GetResourcesSet(request, json => _mailCarrierReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMailCarriersIndex()
        {
            var request = new MailCarriersIndexRequest();
            return await _http.GetResourcesSet(request,
                    json => _mailCarrierReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<MailCarrier>> GetMailCarrierById(int mailCarrierId, Language? language = default)
        {
            var request = new MailCarrierByIdRequest(mailCarrierId, language);
            return await _http.GetResource(request, json => _mailCarrierReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MailCarrier>> GetMailCarriersByIds(
            IReadOnlyCollection<int> mailCarrierIds,
            Language? language = default
        )
        {
            var request = new MailCarriersByIdsRequest(mailCarrierIds, language);
            return await _http
                .GetResourcesSet(request, json => _mailCarrierReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<MailCarrier>> GetMailCarriersByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new MailCarriersByPageRequest(pageIndex, pageSize, language);
            return await _http
                .GetResourcesPage(request, json => _mailCarrierReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
