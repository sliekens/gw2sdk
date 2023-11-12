using GuildWars2.Hero.Equipment.Outfits.Http;

namespace GuildWars2.Hero.Equipment.Outfits;

[PublicAPI]
public sealed class OutfitsClient
{
    private readonly HttpClient httpClient;

    public OutfitsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/outfits

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedOutfitsIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedOutfitsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/outfits

    #region v2/outfits

    public Task<(HashSet<Outfit> Value, MessageContext Context)> GetOutfits(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        OutfitsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetOutfitsIndex(
        CancellationToken cancellationToken = default
    )
    {
        OutfitsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Outfit Value, MessageContext Context)> GetOutfitById(
        int outfitId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        OutfitByIdRequest request = new(outfitId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Outfit> Value, MessageContext Context)> GetOutfitsByIds(
        IReadOnlyCollection<int> outfitIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        OutfitsByIdsRequest request = new(outfitIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Outfit> Value, MessageContext Context)> GetOutfitsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        OutfitsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/outfits
}
