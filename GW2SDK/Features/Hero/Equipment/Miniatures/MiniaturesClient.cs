using GuildWars2.Hero.Equipment.Miniatures.Http;

namespace GuildWars2.Hero.Equipment.Miniatures;

/// <summary>Provides query methods for miniatures and miniatures unlocked on the account.</summary>
[PublicAPI]
public sealed class MiniaturesClient
{
    private readonly HttpClient httpClient;

    public MiniaturesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/minis

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMiniatures(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedMiniaturesRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/minis

    public Task<(HashSet<Miniature> Value, MessageContext Context)> GetMiniatures(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MiniaturesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMiniaturesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MiniaturesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Miniature Value, MessageContext Context)> GetMiniatureById(
        int miniatureId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MiniatureByIdRequest request = new(miniatureId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Miniature> Value, MessageContext Context)> GetMiniaturesByIds(
        IReadOnlyCollection<int> miniatureIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MiniaturesByIdsRequest request = new(miniatureIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Miniature> Value, MessageContext Context)> GetMiniaturesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MiniaturesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
