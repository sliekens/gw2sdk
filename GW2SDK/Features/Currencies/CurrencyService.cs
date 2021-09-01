using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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

        public async Task<IReplicaSet<Currency>> GetCurrencies(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrenciesRequest(language);
            return await http.GetResourcesSet(request, json => currencyReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetCurrenciesIndex(CancellationToken cancellationToken = default)
        {
            var request = new CurrenciesIndexRequest();
            return await http
                .GetResourcesSet(request, json => currencyReader.Id.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Currency>> GetCurrencyById(
            int currencyId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrencyByIdRequest(currencyId, language);
            return await http.GetResource(request, json => currencyReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Currency>> GetCurrenciesByIds(
#if NET
            IReadOnlySet<int> currencyIds,
#else
            IReadOnlyCollection<int> currencyIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrenciesByIdsRequest(currencyIds, language);
            return await http.GetResourcesSet(request, json => currencyReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Currency>> GetCurrenciesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CurrenciesByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => currencyReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
