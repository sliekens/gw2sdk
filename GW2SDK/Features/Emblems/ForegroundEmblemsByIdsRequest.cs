﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Emblems;

[PublicAPI]
public sealed class ForegroundEmblemsByIdsRequest : IHttpRequest<IReplicaSet<Emblem>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/emblem/foregrounds") { AcceptEncoding = "gzip" };

    public ForegroundEmblemsByIdsRequest(IReadOnlyCollection<int> foregroundEmblemIds)
    {
        Check.Collection(foregroundEmblemIds, nameof(foregroundEmblemIds));
        ForegroundEmblemIds = foregroundEmblemIds;
    }

    public IReadOnlyCollection<int> ForegroundEmblemIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Emblem>> SendAsync(
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

        return new ReplicaSet<Emblem>
        {
            Values = json.RootElement.GetSet(entry => entry.GetEmblem(MissingMemberBehavior)),
            Context = response.Headers.GetCollectionContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
