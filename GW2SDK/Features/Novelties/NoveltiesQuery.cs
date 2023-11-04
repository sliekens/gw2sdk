using GuildWars2.Novelties.Http;

namespace GuildWars2.Novelties;

[PublicAPI]
public sealed class NoveltiesQuery
{
    private readonly HttpClient http;

    public NoveltiesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/novelties

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedNoveltiesIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedNoveltiesRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/novelties

    #region v2/novelties

    public Task<(HashSet<Novelty> Value, MessageContext Context)> GetNovelties(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetNoveltiesIndex(
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Novelty Value, MessageContext Context)> GetNoveltyById(
        int noveltyId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltyByIdRequest request = new(noveltyId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Novelty> Value, MessageContext Context)> GetNoveltiesByIds(
        IReadOnlyCollection<int> noveltyIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesByIdsRequest request = new(noveltyIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Novelty> Value, MessageContext Context)> GetNoveltiesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        NoveltiesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/novelties
}
