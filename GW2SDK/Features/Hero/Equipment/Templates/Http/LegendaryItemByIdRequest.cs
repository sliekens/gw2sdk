using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Templates.Http;

internal sealed class LegendaryItemByIdRequest(int legendaryItemId) : IHttpRequest<LegendaryItem>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/legendaryarmory")
    {
        AcceptEncoding = "gzip"
    };

    public int LegendaryItemId { get; } = legendaryItemId;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(LegendaryItem Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", LegendaryItemId },
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
        var value = json.RootElement.GetLegendaryItem(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
