using GuildWars2.Raids.Http;

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

    public Task<(HashSet<string> Value, MessageContext Context)> GetCompletedEncounters(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new CompletedEncountersRequest { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/raids

    public Task<(HashSet<string> Value, MessageContext Context)> GetRaidsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RaidsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaidsByIds(
        IReadOnlyCollection<string> raidIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsByIdsRequest request = new(raidIds) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
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

        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Raid> Value, MessageContext Context)> GetRaids(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaidsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
