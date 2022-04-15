using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Currencies.Http;
using GW2SDK.Currencies.Json;
using GW2SDK.Currencies.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Currencies;

[PublicAPI]
public sealed class Wallet
{
    private readonly HttpClient http;

    public Wallet(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    #region /v2/account/wallet

    [Scope(Permission.Wallet)]
#if NET
    public async Task<IReplica<IReadOnlySet<CurrencyAmount>>> GetWallet(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
#else
    public async Task<IReplica<IReadOnlyCollection<CurrencyAmount>>> GetWallet(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
#endif
    {
        WalletRequest request = new(accessToken);
        return await http.GetResourcesSetSimple(request,
                json => json.RootElement.GetArray(item => CurrencyAmountReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/currencies

    public async Task<IReplicaSet<Currency>> GetCurrencies(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CurrenciesRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => CurrencyReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetCurrenciesIndex(CancellationToken cancellationToken = default)
    {
        CurrenciesIndexRequest request = new();
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
        CurrencyByIdRequest request = new(currencyId, language);
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
        CurrenciesByIdsRequest request = new(currencyIds, language);
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
        CurrenciesByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => CurrencyReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion
}
