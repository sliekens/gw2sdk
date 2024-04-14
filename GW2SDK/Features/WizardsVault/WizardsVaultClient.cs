using GuildWars2.Json;
using GuildWars2.WizardsVault.AstralRewards;
using GuildWars2.WizardsVault.AstralRewards.Http;
using GuildWars2.WizardsVault.Objectives;
using GuildWars2.WizardsVault.Objectives.Http;
using GuildWars2.WizardsVault.Seasons;
using GuildWars2.WizardsVault.Seasons.Http;

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

    #region v2/wizardsvault

    /// <summary>Retrieves the current Wizard's Vault season.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Season Value, MessageContext Context)> GetSeason(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        SeasonRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/wizardsvault

    #region v2/account/wizardsvault/listings

    /// <summary>Retrieves the rewards purchased on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<PurchasedAstralReward> Value, MessageContext Context)>
        GetPurchasedAstralRewards(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        PurchasedAstralRewardsRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/wizardsvault/listings

    #region v2/account/wizardsvault/daily

    /// <summary>Retrieves today's objectives and progress made on the account associated with the access token. This endpoint
    /// is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(DailyObjectivesProgress Value, MessageContext Context)> GetDailyObjectivesProgress(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        DailyObjectivesProgressRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/wizardsvault/daily

    #region v2/account/wizardsvault/weekly

    /// <summary>Retrieves this week's objectives and progress made on the account associated with the access token. This
    /// endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(WeeklyObjectivesProgress Value, MessageContext Context)>
        GetWeeklyObjectivesProgress(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        WeeklyObjectivesProgressRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/wizardsvault/weekly

    #region v2/account/wizardsvault/special

    /// <summary>Retrieves special objectives and progress made on the account associated with the access token. This endpoint
    /// is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(SpecialObjectivesProgress Value, MessageContext Context)>
        GetSpecialObjectivesProgress(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        SpecialObjectivesProgressRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/wizardsvault/weekly

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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AstralRewardsRequest request = new()
        {
            Language = language,
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AstralRewardByIdRequest request = new(astralRewardId)
        {
            Language = language,
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
        IEnumerable<int> astralRewardIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AstralRewardsByIdsRequest request = new(astralRewardIds.ToList())
        {
            Language = language,
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
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        AstralRewardsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/wizardsvault/listings

    #region v2/wizardsvault/objectives

    /// <summary>Retrieves all objectives.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectives(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectivesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all objectives.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetObjectivesIndex(
        CancellationToken cancellationToken = default
    )
    {
        ObjectivesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a reward by its ID.</summary>
    /// <param name="objectiveId">The reward ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Objective Value, MessageContext Context)> GetObjectiveById(
        int objectiveId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectiveByIdRequest request = new(objectiveId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves objectives by their IDs.</summary>
    /// <param name="objectiveIds">The reward IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByIds(
        IEnumerable<int> objectiveIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectivesByIdsRequest request = new(objectiveIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of objectives.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Objective> Value, MessageContext Context)> GetObjectivesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ObjectivesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/wizardsvault/objectives
}
