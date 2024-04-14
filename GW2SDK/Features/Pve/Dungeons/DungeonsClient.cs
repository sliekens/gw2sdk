using GuildWars2.Json;
using GuildWars2.Pve.Dungeons.Http;

namespace GuildWars2.Pve.Dungeons;

/// <summary>Provides query methods for dungeons and completed dungeon paths.</summary>
[PublicAPI]
public sealed class DungeonsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="DungeonsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public DungeonsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/dungeons

    /// <summary>Retrieves the IDs of all dungeon paths that have been completed since the last server reset on the account
    /// associated with the access token. This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetCompletedPaths(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CompletedPathsRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/dungeons

    #region v2/dungeons

    /// <summary>Retrieves the IDs of all dungeons.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetDungeonsIndex(
        CancellationToken cancellationToken = default
    )
    {
        DungeonsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a dungeon by its ID.</summary>
    /// <param name="dungeonId">The dungeon ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Dungeon Value, MessageContext Context)> GetDungeonById(
        string dungeonId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        DungeonByIdRequest request = new(dungeonId);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves dungeons by their IDs.</summary>
    /// <param name="dungeonIds">The dungeon IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Dungeon> Value, MessageContext Context)> GetDungeonsByIds(
        IEnumerable<string> dungeonIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        DungeonsByIdsRequest request = new(dungeonIds.ToList());
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of dungeons.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Dungeon> Value, MessageContext Context)> GetDungeonsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        DungeonsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize
        };

        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves all dungeons.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Dungeon> Value, MessageContext Context)> GetDungeons(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        DungeonsRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/dungeons
}
