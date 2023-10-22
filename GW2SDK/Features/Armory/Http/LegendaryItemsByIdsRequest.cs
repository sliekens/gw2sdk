using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Armory.Http;

internal sealed class LegendaryItemsByIdsRequest : IHttpRequest<Replica<HashSet<LegendaryItem>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/legendaryarmory")
    {
        AcceptEncoding = "gzip"
    };

    public LegendaryItemsByIdsRequest(IReadOnlyCollection<int> legendaryItemIds)
    {
        Check.Collection(legendaryItemIds);
        LegendaryItemIds = legendaryItemIds;
    }

    public IReadOnlyCollection<int> LegendaryItemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<LegendaryItem>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", LegendaryItemIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<LegendaryItem>>
        {
            Value =
                json.RootElement.GetSet(entry => entry.GetLegendaryItem(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
