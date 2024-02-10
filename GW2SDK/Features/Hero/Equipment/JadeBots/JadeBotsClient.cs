using GuildWars2.Hero.Equipment.JadeBots.Http;

namespace GuildWars2.Hero.Equipment.JadeBots;

/// <summary>Provides query methods for jade bot skins and skins unlocked on the account.</summary>
[PublicAPI]
public sealed class JadeBotsClient
{
    private readonly HttpClient httpClient;

    public JadeBotsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/jadebots

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedJadeBotSkins(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedJadeBotsRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/jadebots

    public Task<(HashSet<JadeBotSkin> Value, MessageContext Context)> GetJadeBotSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotSkinsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetJadeBotSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        JadeBotSkinsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(JadeBotSkin Value, MessageContext Context)> GetJadeBotSkinById(
        int jadeBotSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotSkinByIdRequest request = new(jadeBotSkinId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<JadeBotSkin> Value, MessageContext Context)> GetJadeBotSkinsByIds(
        IReadOnlyCollection<int> jadeBotSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotSkinsByIdsRequest request = new(jadeBotSkinIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<JadeBotSkin> Value, MessageContext Context)> GetJadeBotSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotSkinsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
