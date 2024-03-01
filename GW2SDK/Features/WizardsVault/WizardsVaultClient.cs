using GuildWars2.WizardsVault.AstralRewards;
using GuildWars2.WizardsVault.AstralRewards.Http;

namespace GuildWars2.WizardsVault;

/// <summary>Provides query methods for the Wizard's Vault (daily, weekly and special objectives and Astral Rewards).</summary>
[PublicAPI]
public sealed class WizardsVaultClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="WizardsVaultClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public WizardsVaultClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/wizardsvault/listings

    public Task<(HashSet<PurchasedAstralReward> Value, MessageContext Context)>
        GetPurchasedAstralRewards(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        PurchasedAstralRewardsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/wizardsvault/listings

    #region v2/wizardsvault/listings

    /// <summary>Retrieves all rewards.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AstralReward> Value, MessageContext Context)> GetAstralRewards(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AstralRewardsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all rewards.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetAstralRewardsIndex(
        CancellationToken cancellationToken = default
    )
    {
        AstralRewardsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a reward by its ID.</summary>
    /// <param name="astralRewardId">The reward ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(AstralReward Value, MessageContext Context)> GetAstralRewardById(
        int astralRewardId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AstralRewardByIdRequest request = new(astralRewardId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves rewards by their IDs.</summary>
    /// <param name="astralRewardIds">The reward IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AstralReward> Value, MessageContext Context)> GetAstralRewardsByIds(
        IReadOnlyCollection<int> astralRewardIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AstralRewardsByIdsRequest request = new(astralRewardIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of rewards.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<AstralReward> Value, MessageContext Context)> GetAstralRewardsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AstralRewardsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/wizardsvault/listings
}
