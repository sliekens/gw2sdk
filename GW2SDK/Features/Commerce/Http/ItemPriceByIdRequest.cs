﻿using GuildWars2.Commerce.Prices;
using GuildWars2.Http;

namespace GuildWars2.Commerce.Http;

internal sealed class ItemPriceByIdRequest : IHttpRequest2<ItemPrice>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/commerce/prices")
    {
        AcceptEncoding = "gzip"
    };

    public ItemPriceByIdRequest(int itemId)
    {
        ItemId = itemId;
    }

    public int ItemId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(ItemPrice Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetItemPrice(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
