using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Currencies.Impl;
using GW2SDK.Http;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public sealed class CurrencyService
    {
        private static readonly IJsonReader<int> KeyReader = new Int32JsonReader();
        private static readonly IJsonReader<IEnumerable<int>> KeyArrayReader = new JsonArrayReader<int>(KeyReader);
        private static readonly IJsonReader<Currency> ValueReader = CurrencyJsonReader.Instance;
        private static readonly IJsonReader<IEnumerable<Currency>> ValueArrayReader = new JsonArrayReader<Currency>(ValueReader);

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
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Currency>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<Currency>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetCurrenciesIndex()
        {
            var request = new CurrenciesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(KeyArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Currency?> GetCurrencyById(int currencyId)
        {
            var request = new CurrencyByIdRequest(currencyId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            return ValueReader.Read(jsonDocument.RootElement);
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
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Currency>(context.ResultCount);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferCollection<Currency>(list, context);
        }

        public async Task<IDataTransferPage<Currency>> GetCurrenciesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Currency>(pageContext.PageSize);
            list.AddRange(ValueArrayReader.Read(jsonDocument.RootElement));
            return new DataTransferPage<Currency>(list, pageContext);
        }
    }
}
