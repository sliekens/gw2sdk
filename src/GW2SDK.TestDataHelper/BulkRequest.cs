using System.IO.Compression;
using System.Text.Json;

using GuildWars2.Http;

namespace GuildWars2.TestDataHelper;

internal sealed class BulkRequest(Uri requestUri)
{
    public required IReadOnlyCollection<int> Ids { get; init; }

    public async Task<JsonDocument> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        QueryBuilder search = new()
        {
            { "ids", string.Join(",", Ids) },
            { "v", "3" }
        };

        Uri location = new(requestUri.ToString() + search.Build(), UriKind.Relative);
        using HttpRequestMessage request = new(HttpMethod.Get, location);
        request.Headers.AcceptEncoding.ParseAdd("gzip");

        using HttpResponseMessage response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        Stream content = await response.Content.ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);
        content = new GZipStream(content, CompressionMode.Decompress);
        await using (content)
        {
            return await JsonDocument.ParseAsync(content, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
