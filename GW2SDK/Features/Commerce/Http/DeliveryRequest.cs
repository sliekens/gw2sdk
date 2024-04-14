using GuildWars2.Commerce.Delivery;
using GuildWars2.Http;

namespace GuildWars2.Commerce.Http;

internal sealed class DeliveryRequest : IHttpRequest<DeliveryBox>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/commerce/delivery")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    
    public async Task<(DeliveryBox Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetDeliveryBox();
        return (value, new MessageContext(response));
    }
}
