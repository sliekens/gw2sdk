using GuildWars2.Json;
using GuildWars2.Pve.Raids.Http;

namespace GuildWars2.Pve.Raids;

/// <summary>Provides query methods for raids and completed encounters.</summary>
[PublicAPI]
public sealed class RaidsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="RaidsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public RaidsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/raids

    /// <summary>Retrieves the IDs of all raid encounters that have been completed since the last weekly reset on the account
    /// associated with the access token. This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetCompletedEncounters(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new CompletedEncountersRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/raids

    /// <summary>Retrieves the IDs of all raids.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetRaidsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RaidsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a raid by its ID.</summary>
    /// <param name="raidId">The raid ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Raid Value, MessageContext Context)> GetRaidById(
        string raidId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RaidByIdRequest request = new(raidId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves raids by their IDs.</summary>
    /// <param name="raidIds">The raid IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaidsByIds(
        IEnumerable<string> raidIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RaidsByIdsRequest request = new(raidIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of raids.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaidsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RaidsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize
        };

        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves all raids.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaids(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        RaidsRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
