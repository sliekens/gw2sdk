﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.ItemStats.Http;

internal sealed class ItemStatsByIdsRequest : IHttpRequest<Replica<HashSet<ItemStat>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/itemstats")
    {
        AcceptEncoding = "gzip"
    };

    public ItemStatsByIdsRequest(IReadOnlyCollection<int> itemStatIds)
    {
        Check.Collection(itemStatIds);
        ItemStatIds = itemStatIds;
    }

    public IReadOnlyCollection<int> ItemStatIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<ItemStat>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ItemStatIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetItemStat(MissingMemberBehavior));
        return new Replica<HashSet<ItemStat>>
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
