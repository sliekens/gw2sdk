﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Files.Http;

internal sealed class FilesByIdsRequest : IHttpRequest<HashSet<Asset>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/files")
    {
        AcceptEncoding = "gzip"
    };

    public FilesByIdsRequest(IReadOnlyCollection<string> fileIds)
    {
        Check.Collection(fileIds);
        FileIds = fileIds;
    }

    public IReadOnlyCollection<string> FileIds { get; }

    
    public async Task<(HashSet<Asset> Value, MessageContext Context)> SendAsync(
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
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetAsset());
        return (value, new MessageContext(response));
    }
}
