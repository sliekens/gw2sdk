using GuildWars2.Hero.Races.Http;

namespace GuildWars2.Hero.Races;

/// <summary>Provides query method for playable races.</summary>
[PublicAPI]
public sealed class RacesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="RacesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public RacesClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves all races.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Race> Value, MessageContext Context)> GetRaces(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RacesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all races.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<string> Value, MessageContext Context)> GetRacesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RacesIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a race by its name.</summary>
    /// <param name="raceName">The race name.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(Race Value, MessageContext Context)> GetRaceByName(
        RaceName raceName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RaceByNameRequest request = new(raceName)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves races by their name.</summary>
    /// <param name="raceNames">The race names.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Race> Value, MessageContext Context)> GetRacesByNames(
        IEnumerable<RaceName> raceNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RacesByNamesRequest request = new(raceNames.ToList())
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves a page of races.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(HashSet<Race> Value, MessageContext Context)> GetRacesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RacesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
