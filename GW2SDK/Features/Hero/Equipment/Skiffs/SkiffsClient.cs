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

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedSkiffSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedSkiffSkinsRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/skiffs

    public Task<(HashSet<SkiffSkin> Value, MessageContext Context)> GetSkiffSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffSkinsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetSkiffSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        SkiffSkinsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(SkiffSkin Value, MessageContext Context)> GetSkiffSkinById(
        int skiffSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffSkinByIdRequest request = new(skiffSkinId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<SkiffSkin> Value, MessageContext Context)> GetSkiffSkinsByIds(
        IReadOnlyCollection<int> skiffSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffSkinsByIdsRequest request = new(skiffSkinIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<SkiffSkin> Value, MessageContext Context)> GetSkiffSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        SkiffSkinsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
