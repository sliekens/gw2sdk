using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Stories.Http;

internal sealed class SeasonsByIdsRequest : IHttpRequest<Replica<HashSet<Season>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/stories/seasons") { AcceptEncoding = "gzip" };

    public SeasonsByIdsRequest(IReadOnlyCollection<string> seasonIds)
    {
        Check.Collection(seasonIds);
        SeasonIds = seasonIds;
    }

    public IReadOnlyCollection<string> SeasonIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Season>>> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetSeason(MissingMemberBehavior));
        return new Replica<HashSet<Season>>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
