using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Currencies.Http;
using GW2SDK.Currencies.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    public sealed class CurrencyService
    {
        private readonly HttpClient http;

        public CurrencyService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<Currency>> GetCurrencies(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrenciesRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => CurrencyReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetCurrenciesIndex(CancellationToken cancellationToken = default)
        {
            var request = new CurrenciesIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Currency>> GetCurrencyById(
            int currencyId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrencyByIdRequest(currencyId, language);
            return await http.GetResource(request,
                    json => CurrencyReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Currency>> GetCurrenciesByIds(
#if NET
            IReadOnlySet<int> currencyIds,
#else
            IReadOnlyCollection<int> currencyIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrenciesByIdsRequest(currencyIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => CurrencyReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Currency>> GetCurrenciesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => CurrencyReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
