using GuildWars2.Races.Http;

namespace GuildWars2.Races;

[PublicAPI]
public sealed class RacesQuery
{
    private readonly HttpClient http;

    public RacesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<string> Value, MessageContext Context)> GetRacesIndex(
        CancellationToken cancellationToken = default
    )
    {
        RacesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Race> Value, MessageContext Context)> GetRacesByNames(
        IReadOnlyCollection<RaceName> raceNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RacesByNamesRequest request = new(raceNames)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

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
        return request.SendAsync(http, cancellationToken);
    }
}
