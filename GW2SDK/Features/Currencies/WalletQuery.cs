using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Currencies;

[PublicAPI]
public sealed class WalletQuery
{
    private readonly HttpClient http;

    public WalletQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/wallet

    [Scope(Permission.Wallet)]
    public Task<IReplica<IReadOnlyCollection<CurrencyAmount>>> GetWallet(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        WalletRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/currencies

    public Task<IReplicaSet<Currency>> GetCurrencies(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CurrenciesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetCurrenciesIndex(CancellationToken cancellationToken = default)
    {
        CurrenciesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Currency>> GetCurrencyById(
        int currencyId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CurrencyByIdRequest request = new(currencyId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<Currency>> GetCurrenciesByIds(
        IReadOnlyCollection<int> currencyIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CurrenciesByIdsRequest request = new(currencyIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<Currency>> GetCurrenciesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CurrenciesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
