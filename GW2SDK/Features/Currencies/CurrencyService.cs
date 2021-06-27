using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Currencies.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public sealed class CurrencyService
    {
        private readonly ICurrencyReader _currencyReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public CurrencyService(HttpClient http, ICurrencyReader currencyReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _currencyReader = currencyReader ?? throw new ArgumentNullException(nameof(currencyReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IDataTransferSet<Currency>> GetCurrencies()
        {
            var request = new CurrenciesRequest();
            return await _http.GetResourcesSet(request, json => _currencyReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetCurrenciesIndex()
        {
            var request = new CurrenciesIndexRequest();
            return await _http.GetResourcesSet(request, json => _currencyReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<Currency> GetCurrencyById(int currencyId)
        {
            var request = new CurrencyByIdRequest(currencyId);
            return await _http.GetResource(request, json => _currencyReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Currency>> GetCurrenciesByIds(IReadOnlyCollection<int> currencyIds)
        {
            var request = new CurrenciesByIdsRequest(currencyIds);
            return await _http.GetResourcesSet(request, json => _currencyReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Currency>> GetCurrenciesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _currencyReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
