﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Finishers.Http;

internal sealed class FinishersByIdsRequest : IHttpRequest<Replica<HashSet<Finisher>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/finishers") { AcceptEncoding = "gzip" };

    public FinishersByIdsRequest(IReadOnlyCollection<int> finisherIds)
    {
        Check.Collection(finisherIds);
        FinisherIds = finisherIds;
    }

    public IReadOnlyCollection<int> FinisherIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Finisher>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", FinisherIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Finisher>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetFinisher(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}