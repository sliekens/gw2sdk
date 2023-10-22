using GuildWars2.Outfits.Http;

namespace GuildWars2.Outfits;

[PublicAPI]
public sealed class OutfitsQuery
{
    private readonly HttpClient http;

    public OutfitsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/outfits

    public Task<Replica<HashSet<int>>> GetUnlockedOutfitsIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedOutfitsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/outfits

    #region v2/outfits

    public Task<Replica<HashSet<Outfit>>> GetOutfits(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetOutfitsIndex(
        CancellationToken cancellationToken = default
    )
    {
        OutfitsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Outfit>> GetOutfitById(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Outfit>>> GetOutfitsByIds(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Outfit>>> GetOutfitsByPage(
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
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/outfits
}
