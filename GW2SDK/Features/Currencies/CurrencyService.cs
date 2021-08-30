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
        private readonly ICurrencyReader currencyReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public CurrencyService(
            HttpClient http,
            ICurrencyReader currencyReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.currencyReader = currencyReader ?? throw new ArgumentNullException(nameof(currencyReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Currency>> GetCurrencies(Language? language = default)
        {
            var request = new CurrenciesRequest(language);
            return await http.GetResourcesSet(request, json => currencyReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetCurrenciesIndex()
        {
            var request = new CurrenciesIndexRequest();
            return await http
                .GetResourcesSet(request, json => currencyReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Currency>> GetCurrencyById(int currencyId, Language? language = default)
        {
            var request = new CurrencyByIdRequest(currencyId, language);
            return await http.GetResource(request, json => currencyReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Currency>> GetCurrenciesByIds(
            IReadOnlyCollection<int> currencyIds,
            Language? language = default
        )
        {
            var request = new CurrenciesByIdsRequest(currencyIds, language);
            return await http.GetResourcesSet(request, json => currencyReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Currency>> GetCurrenciesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => currencyReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
