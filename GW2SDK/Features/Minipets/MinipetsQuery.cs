using GuildWars2.Minipets.Http;

namespace GuildWars2.Minipets;

[PublicAPI]
public sealed class MinipetsQuery
{
    private readonly HttpClient http;

    public MinipetsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/minis

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMinipets(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedMinipetsRequest { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/minis

    public Task<(HashSet<Minipet> Value, MessageContext Context)> GetMinipets(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMinipetsIndex(
        CancellationToken cancellationToken = default
    )
    {
        MinipetsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Minipet Value, MessageContext Context)> GetMinipetById(
        int minipetId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetByIdRequest request = new(minipetId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Minipet> Value, MessageContext Context)> GetMinipetsByIds(
        IReadOnlyCollection<int> minipetIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetsByIdsRequest request = new(minipetIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Minipet> Value, MessageContext Context)> GetMinipetsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MinipetsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
