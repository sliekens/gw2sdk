using GuildWars2.Http;

namespace GuildWars2.Hero.Inventories.Http;

internal sealed class SharedInventoryRequest : IHttpRequest<Inventory>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account/inventory")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Inventory Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetInventory(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
