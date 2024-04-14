﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Files.Http;

internal sealed class FilesByPageRequest(int pageIndex) : IHttpRequest<HashSet<Asset>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/files") { AcceptEncoding = "gzip" };

    public int PageIndex { get; } = pageIndex;

    public int? PageSize { get; init; }

    
    public async Task<(HashSet<Asset> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with { Arguments = search },
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
