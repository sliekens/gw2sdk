using GuildWars2.Http;

namespace GuildWars2.Hero.Races.Http;

internal sealed class RaceByNameRequest(RaceName raceName) : IHttpRequest<Race>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/races")
    {
        AcceptEncoding = "gzip"
    };

    public RaceName RaceName { get; } = raceName;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Race Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", RaceName.ToString() },
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
        var value = json.RootElement.GetRace(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
