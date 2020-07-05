using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.MailCarriers.Impl;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.MailCarriers
{
    [PublicAPI]
    public sealed class MailCarrierService
    {
        private readonly HttpClient _http;

        public MailCarrierService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<MailCarrier>> GetMailCarriers()
        {
            var request = new MailCarriersRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<MailCarrier>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<MailCarrier>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetMailCarriersIndex()
        {
            var request = new MailCarriersIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<MailCarrier?> GetMailCarrierById(int mailCarrierId)
        {
            var request = new MailCarrierByIdRequest(mailCarrierId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<MailCarrier>(json, Json.DefaultJsonSerializerSettings);
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
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<MailCarrier>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<MailCarrier>(list, context);
        }

        public async Task<IDataTransferPage<MailCarrier>> GetMailCarriersByPage(int pageIndex, int? pageSize = null)
        {
            var request = new MailCarriersByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<MailCarrier>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<MailCarrier>(list, pageContext);
        }
    }
}
