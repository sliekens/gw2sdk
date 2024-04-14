using GuildWars2.Hero.Masteries.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

/// <summary>Provides query methods for masteries, mastery points and mastery progress on the account.</summary>
[PublicAPI]
public sealed class MasteriesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MasteriesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MasteriesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/masteries

    /// <summary>Retrieves mastery track progress on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MasteryTrackProgress> Value, MessageContext Context)>
        GetMasteryTrackProgress(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MasteryProgressRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/masteries

    #region v2/account/mastery/points

    /// <summary>Retrieves information about earned and spent mastery points on the account associated with the access token.
    /// This endpoint is only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MasteryPointsProgress Value, MessageContext Context)> GetMasteryPointsProgress(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MasteryPointsProgressRequest request = new()
        {
            AccessToken = accessToken,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/account/mastery/points

    #region v2/masteries

    /// <summary>Retrieves all mastery tracks.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MasteryTrack> Value, MessageContext Context)> GetMasteryTracks(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MasteriesRequest request = new()
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all mastery tracks.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<int> Value, MessageContext Context)> GetMasteryTracksIndex(
        CancellationToken cancellationToken = default
    )
    {
        MasteriesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a mastery track by its ID.</summary>
    /// <param name="masteryTrackId">The mastery track ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(MasteryTrack Value, MessageContext Context)> GetMasteryTrackById(
        int masteryTrackId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MasteryByIdRequest request = new(masteryTrackId)
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves mastery tracks by their IDs.</summary>
    /// <param name="masteryTrackIds">The mastery track IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<MasteryTrack> Value, MessageContext Context)> GetMasteryTracksByIds(
        IEnumerable<int> masteryTrackIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        MasteriesByIdsRequest request = new(masteryTrackIds.ToList())
        {
            Language = language,
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    #endregion v2/masteries
}
