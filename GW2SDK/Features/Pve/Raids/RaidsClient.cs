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

    public Task<(HashSet<string> Value, MessageContext Context)> GetRaidsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RaidsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Raid Value, MessageContext Context)> GetRaidById(
        string raidId,
        CancellationToken cancellationToken = default
    )
    {
        RaidByIdRequest request = new(raidId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaidsByIds(
        IReadOnlyCollection<string> raidIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsByIdsRequest request = new(raidIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaidsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };

        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaids(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
