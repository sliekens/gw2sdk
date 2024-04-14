using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons.Http;

internal sealed class SeasonsByIdsRequest : IHttpRequest<HashSet<Season>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/seasons") { AcceptEncoding = "gzip" };

    public SeasonsByIdsRequest(IReadOnlyCollection<string> seasonIds)
    {
        Check.Collection(seasonIds);
        SeasonIds = seasonIds;
    }

    public IReadOnlyCollection<string> SeasonIds { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<Season> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SeasonIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetSeason());
        return (value, new MessageContext(response));
    }
}
