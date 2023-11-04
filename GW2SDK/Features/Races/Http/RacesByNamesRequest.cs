using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Races.Http;

internal sealed class RacesByNamesRequest : IHttpRequest2<HashSet<Race>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/races")
    {
        AcceptEncoding = "gzip"
    };

    public RacesByNamesRequest(IReadOnlyCollection<RaceName> raceIds)
    {
        Check.Collection(raceIds);
        RaceIds = raceIds;
    }

    public IReadOnlyCollection<RaceName> RaceIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Race> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        {
                            "ids", RaceIds.Select(
#if NET
                                id => Enum.GetName(id)!
#else
                                id => Enum.GetName(typeof(RaceName), id)!
#endif
                            )
                        },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetRace(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
