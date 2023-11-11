using GuildWars2.Hero.Wallet.Http;

namespace GuildWars2.Hero.Wallet;

[PublicAPI]
public sealed class WalletClient
{
    private readonly HttpClient httpClient;

    public WalletClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/wallet

    public Task<(HashSet<CurrencyAmount> Value, MessageContext Context)> GetWallet(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/currencies

    public Task<(HashSet<Currency> Value, MessageContext Context)> GetCurrencies(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetCurrenciesIndex(
        CancellationToken cancellationToken = default
    )
    {
        CurrenciesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Currency Value, MessageContext Context)> GetCurrencyById(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Currency> Value, MessageContext Context)> GetCurrenciesByIds(
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
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Currency> Value, MessageContext Context)> GetCurrenciesByPage(
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

        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
