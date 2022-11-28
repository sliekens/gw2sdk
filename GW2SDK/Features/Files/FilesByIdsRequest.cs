﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Files;

[PublicAPI]
public sealed class FilesByIdsRequest : IHttpRequest<IReplicaSet<File>>
{
    private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "v2/files")
    {
        AcceptEncoding = "gzip"
    };

    public FilesByIdsRequest(IReadOnlyCollection<string> fileIds)
    {
        Check.Collection(fileIds, nameof(fileIds));
        FileIds = fileIds;
    }

    public IReadOnlyCollection<string> FileIds { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<File>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", FileIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => FileJson.GetFile(entry, MissingMemberBehavior));
        return new ReplicaSet<File>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
