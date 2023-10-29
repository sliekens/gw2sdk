﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Armory.Http;

internal sealed class
    BoundLegendaryItemsRequest : IHttpRequest<Replica<HashSet<BoundLegendaryItem>>>
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

    public async Task<Replica<HashSet<BoundLegendaryItem>>> SendAsync(
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
        return new Replica<HashSet<BoundLegendaryItem>>
        {
            Value =
                json.RootElement.GetSet(
                    entry => entry.GetBoundLegendaryItem(MissingMemberBehavior)
                ),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
