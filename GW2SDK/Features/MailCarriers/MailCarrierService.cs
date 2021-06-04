using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.MailCarriers.Http;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    public sealed class MailCarrierService
    {
        private readonly HttpClient _http;

        private readonly IMailCarrierReader _mailCarrierReader;

        public MailCarrierService(HttpClient http, IMailCarrierReader mailCarrierReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _mailCarrierReader = mailCarrierReader ?? throw new ArgumentNullException(nameof(mailCarrierReader));
        }

        public async Task<IDataTransferSet<MailCarrier>> GetMailCarriers()
        {
            var request = new MailCarriersRequest();
            return await _http.GetResourcesSet(request, json => _mailCarrierReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetMailCarriersIndex()
        {
            var request = new MailCarriersIndexRequest();
            return await _http.GetResourcesSet(request, json => _mailCarrierReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<MailCarrier> GetMailCarrierById(int mailCarrierId)
        {
            var request = new MailCarrierByIdRequest(mailCarrierId);
            return await _http.GetResource(request, json => _mailCarrierReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<MailCarrier>> GetMailCarriersByIds(IReadOnlyCollection<int> mailCarrierIds)
        {
            var request = new MailCarriersByIdsRequest(mailCarrierIds);
            return await _http.GetResourcesSet(request, json => _mailCarrierReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<MailCarrier>> GetMailCarriersByPage(int pageIndex, int? pageSize = null)
        {
            var request = new MailCarriersByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _mailCarrierReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
