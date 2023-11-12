using GuildWars2.Hero.Equipment.Skiffs.Http;

namespace GuildWars2.Hero.Equipment.Skiffs;

/// <summary>Provides query methods for skiffs and skiffs unlocked on the account.</summary>
[PublicAPI]
public sealed class SkiffsClient
{
    private readonly HttpClient httpClient;

    public SkiffsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/skiffs

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedSkiffs(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedSkiffsRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/skiffs

    public Task<(HashSet<Skiff> Value, MessageContext Context)> GetSkiffs(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetSkiffsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SkiffsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Skiff Value, MessageContext Context)> GetSkiffById(
        int skiffId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffByIdRequest request = new(skiffId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Skiff> Value, MessageContext Context)> GetSkiffsByIds(
        IReadOnlyCollection<int> skiffIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffsByIdsRequest request = new(skiffIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Skiff> Value, MessageContext Context)> GetSkiffsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
