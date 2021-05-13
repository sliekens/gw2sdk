using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Currencies.Http;
using GW2SDK.Http;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public sealed class CurrencyService
    {
        private readonly HttpClient _http;
        private readonly ICurrencyReader _currencyReader;

        public CurrencyService(HttpClient http, ICurrencyReader currencyReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _currencyReader = currencyReader ?? throw new ArgumentNullException(nameof(currencyReader));
        }

        public async Task<IDataTransferCollection<Currency>> GetCurrencies()
        {
            var request = new CurrenciesRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Currency>(context.ResultCount);
            list.AddRange(_currencyReader.ReadArray(json));
            return new DataTransferCollection<Currency>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetCurrenciesIndex()
        {
            var request = new CurrenciesIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_currencyReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Currency> GetCurrencyById(int currencyId)
        {
            var request = new CurrencyByIdRequest(currencyId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _currencyReader.Read(json);
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
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Currency>(context.ResultCount);
            list.AddRange(_currencyReader.ReadArray(json));
            return new DataTransferCollection<Currency>(list, context);
        }

        public async Task<IDataTransferPage<Currency>> GetCurrenciesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Currency>(pageContext.PageSize);
            list.AddRange(_currencyReader.ReadArray(json));
            return new DataTransferPage<Currency>(list, pageContext);
        }
    }
}
