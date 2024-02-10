using GuildWars2.Hero.Equipment.Gliders.Http;

namespace GuildWars2.Hero.Equipment.Gliders;

/// <summary>Provides query methods for glider skins and skins unlocked on the account.</summary>
[PublicAPI]
public sealed class GlidersClient
{
    private readonly HttpClient httpClient;

    public GlidersClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<GliderSkin> Value, MessageContext Context)> GetGliderSkins(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GliderSkinsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetGliderSkinsIndex(
        CancellationToken cancellationToken = default
    )
    {
        GliderSkinsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(GliderSkin Value, MessageContext Context)> GetGliderSkinById(
        int gliderSkinId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GliderSkinByIdRequest request = new(gliderSkinId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<GliderSkin> Value, MessageContext Context)> GetGliderSkinsByIds(
        IReadOnlyCollection<int> gliderSkinIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GliderSkinsByIdsRequest request = new(gliderSkinIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<GliderSkin> Value, MessageContext Context)> GetGliderSkinsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        GliderSkinsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
