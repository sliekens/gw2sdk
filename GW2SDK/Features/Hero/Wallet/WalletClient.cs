using GuildWars2.Hero.Wallet.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Wallet;

/// <summary>Provides query methods for currencies in the game and in the account wallet.</summary>
[PublicAPI]
public sealed class WalletClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="WalletClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public WalletClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/wallet

    /// <summary>Retrieves the amount of currency in the wallet of the account associated with the access token. This endpoint
    /// is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<CurrencyAmount> Value, MessageContext Context)> GetWallet(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        WalletRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/currencies

    /// <summary>Retrieves all currencies.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Currency> Value, MessageContext Context)> GetCurrencies(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        CurrenciesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all currencies.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetCurrenciesIndex(
        CancellationToken cancellationToken = default
    )
    {
        CurrenciesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a currency by its ID.</summary>
    /// <param name="currencyId">The currency ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Currency Value, MessageContext Context)> GetCurrencyById(
        int currencyId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        CurrencyByIdRequest request = new(currencyId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves currencies by their IDs.</summary>
    /// <param name="currencyIds">The currency IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Currency> Value, MessageContext Context)> GetCurrenciesByIds(
        IEnumerable<int> currencyIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        CurrenciesByIdsRequest request = new(currencyIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of currencies.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Currency> Value, MessageContext Context)> GetCurrenciesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        CurrenciesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language
        };

        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
