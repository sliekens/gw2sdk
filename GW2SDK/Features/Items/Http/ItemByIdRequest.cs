﻿using GuildWars2.Http;

namespace GuildWars2.Items.Http;

internal sealed class ItemByIdRequest : IHttpRequest<Replica<Item>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/items")
    {
        AcceptEncoding = "gzip"
    };

    public ItemByIdRequest(int itemId)
    {
        ItemId = itemId;
    }

    public int ItemId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Item>> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetItem(MissingMemberBehavior);
        return new Replica<Item>
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
