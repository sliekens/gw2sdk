using GuildWars2.Hero.Equipment.Outfits.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Outfits;

/// <summary>Provides query methods for outfits and outfits unlocked on the account.</summary>
[PublicAPI]
public sealed class OutfitsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="OutfitsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public OutfitsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/outfits

    /// <summary>Retrieves the IDs of outfits unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedOutfits(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedOutfitsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/outfits

    #region v2/outfits

    /// <summary>Retrieves all outfits.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Outfit> Value, MessageContext Context)> GetOutfits(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        OutfitsRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all outfits.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetOutfitsIndex(
        CancellationToken cancellationToken = default
    )
    {
        OutfitsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves an outfit by its ID.</summary>
    /// <param name="outfitId">The outfit ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Outfit Value, MessageContext Context)> GetOutfitById(
        int outfitId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        OutfitByIdRequest request = new(outfitId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves outfits by their IDs.</summary>
    /// <param name="outfitIds">The outfit IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Outfit> Value, MessageContext Context)> GetOutfitsByIds(
        IEnumerable<int> outfitIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        OutfitsByIdsRequest request = new(outfitIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of outfits.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Outfit> Value, MessageContext Context)> GetOutfitsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        OutfitsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/outfits
}
