﻿using GuildWars2.Http;

namespace GuildWars2.Files.Http;

internal sealed class FileByIdRequest : IHttpRequest<Replica<Asset>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/files") { AcceptEncoding = "gzip" };

    public FileByIdRequest(string fileId)
    {
        FileId = fileId;
    }

    public string FileId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Asset>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", FileId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetAsset(MissingMemberBehavior);
        return new Replica<Asset>
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
