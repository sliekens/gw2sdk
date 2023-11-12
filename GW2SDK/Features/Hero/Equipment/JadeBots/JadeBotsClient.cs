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

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedJadeBots(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        var request = new UnlockedJadeBotsRequest { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion

    #region v2/jadebots

    public Task<(HashSet<JadeBot> Value, MessageContext Context)> GetJadeBots(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetJadeBotsIndex(
        CancellationToken cancellationToken = default
    )
    {
        JadeBotsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(JadeBot Value, MessageContext Context)> GetJadeBotById(
        int jadeBotId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotByIdRequest request = new(jadeBotId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<JadeBot> Value, MessageContext Context)> GetJadeBotsByIds(
        IReadOnlyCollection<int> jadeBotIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotsByIdsRequest request = new(jadeBotIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<JadeBot> Value, MessageContext Context)> GetJadeBotsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JadeBotsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion
}
