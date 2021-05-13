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

        public async Task<IDataTransferCollection<MailCarrier>> GetMailCarriers()
        {
            var request = new MailCarriersRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<MailCarrier>(context.ResultCount);
            list.AddRange(_mailCarrierReader.ReadArray(json));
            return new DataTransferCollection<MailCarrier>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetMailCarriersIndex()
        {
            var request = new MailCarriersIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_mailCarrierReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<MailCarrier> GetMailCarrierById(int mailCarrierId)
        {
            var request = new MailCarrierByIdRequest(mailCarrierId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _mailCarrierReader.Read(json);
        }

        public async Task<IDataTransferCollection<MailCarrier>> GetMailCarriersByIds(IReadOnlyCollection<int> mailCarrierIds)
        {
            if (mailCarrierIds is null)
            {
                throw new ArgumentNullException(nameof(mailCarrierIds));
            }

            if (mailCarrierIds.Count == 0)
            {
                throw new ArgumentException("Mail carrier IDs cannot be an empty collection.", nameof(mailCarrierIds));
            }

            var request = new MailCarriersByIdsRequest(mailCarrierIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<MailCarrier>(context.ResultCount);
            list.AddRange(_mailCarrierReader.ReadArray(json));
            return new DataTransferCollection<MailCarrier>(list, context);
        }

        public async Task<IDataTransferPage<MailCarrier>> GetMailCarriersByPage(int pageIndex, int? pageSize = null)
        {
            var request = new MailCarriersByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<MailCarrier>(pageContext.PageSize);
            list.AddRange(_mailCarrierReader.ReadArray(json));
            return new DataTransferPage<MailCarrier>(list, pageContext);
        }
    }
}
