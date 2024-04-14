using GuildWars2.Http;

namespace GuildWars2.Items.Http;

internal sealed class ItemByIdRequest(int itemId) : IHttpRequest<Item>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public int ItemId { get; } = itemId;

    public Language? Language { get; init; }

    
    public async Task<(Item Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    AcceptLanguage = Language?.Alpha2Code,
                    Arguments = new QueryBuilder
                    {
                        { "id", ItemId },
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
        var value = json.RootElement.GetItem();
        return (value, new MessageContext(response));
    }
}
