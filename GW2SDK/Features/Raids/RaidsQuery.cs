namespace GuildWars2.Raids;

[PublicAPI]
public sealed class RaidsQuery
{
    private readonly HttpClient http;

    public RaidsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/raids

    [Scope(Permission.Progression)]
    public Task<Replica<HashSet<string>>> GetCompletedEncounters(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new CompletedEncountersRequest { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/raids

    public Task<Replica<HashSet<string>>> GetRaidsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RaidsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Raid>> GetRaidById(
        string raidId,
        CancellationToken cancellationToken = default
    )
    {
        RaidByIdRequest request = new(raidId)
        {
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Raid>>> GetRaidsByIds(
        IReadOnlyCollection<string> raidIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsByIdsRequest request = new(raidIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Raid>>> GetRaidsByPage(
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

        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Raid>>> GetRaids(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
