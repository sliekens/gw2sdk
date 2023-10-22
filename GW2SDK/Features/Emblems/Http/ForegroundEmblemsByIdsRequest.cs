﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Emblems.Http;

internal sealed class ForegroundEmblemsByIdsRequest : IHttpRequest<Replica<HashSet<Emblem>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

    public ForegroundEmblemsByIdsRequest(IReadOnlyCollection<int> foregroundEmblemIds)
    {
        Check.Collection(foregroundEmblemIds);
        ForegroundEmblemIds = foregroundEmblemIds;
    }

    public IReadOnlyCollection<int> ForegroundEmblemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Emblem>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ForegroundEmblemIds },
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
        return new Replica<HashSet<Emblem>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetEmblem(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
