using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Currencies.Impl;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public sealed class CurrencyService
    {
        private readonly HttpClient _http;

        public CurrencyService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<Currency>> GetCurrencies()
        {
            var request = new CurrenciesRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Currency>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Currency>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetCurrenciesIndex()
        {
            var request = new CurrenciesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Currency?> GetCurrencyById(int currencyId)
        {
            var request = new CurrencyByIdRequest(currencyId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Currency>(json, Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Currency>> GetCurrenciesByIds(IReadOnlyCollection<int> currencyIds)
        {
            if (currencyIds is null)
            {
                throw new ArgumentNullException(nameof(currencyIds));
            }

            if (currencyIds.Count == 0)
            {
                throw new ArgumentException("Currency IDs cannot be an empty collection.", nameof(currencyIds));
            }

            var request = new CurrenciesByIdsRequest(currencyIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Currency>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Currency>(list, context);
        }

        public async Task<IDataTransferPage<Currency>> GetCurrenciesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Currency>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Currency>(list, pageContext);
        }
    }
}
