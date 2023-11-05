using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Http;

internal sealed class
    BoundLegendaryItemsRequest : IHttpRequest<HashSet<BoundLegendaryItem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/account/legendaryarmory")
        {
            Arguments = new QueryBuilder
            {
                { "ids", "all" },
                { "v", SchemaVersion.Recommended }
            }
        };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<BoundLegendaryItem> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => BoundLegendaryItemJson.GetBoundLegendaryItem(entry, MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
