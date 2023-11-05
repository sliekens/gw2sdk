using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Http;

internal sealed class LegendaryItemsByIdsRequest : IHttpRequest<HashSet<LegendaryItem>>
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

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<LegendaryItem> Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => LegendaryItemJson.GetLegendaryItem(entry, MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
