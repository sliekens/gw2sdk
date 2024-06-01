using System.IO.Compression;
using System.Text.Json;
using GuildWars2.Http;

namespace GuildWars2.TestDataHelper;

public class BulkRequest(string requestUri)
{
    public required IReadOnlyCollection<int> Ids { get; init; }

    public async Task<JsonDocument> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "ids", string.Join(",", Ids) },
            { "v", "3" }
        };

        var message = new HttpRequestMessage(HttpMethod.Get, requestUri + search);
        message.Headers.AcceptEncoding.ParseAdd("gzip");

        using var response = await httpClient.SendAsync(
                message,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);
        content = new GZipStream(content, CompressionMode.Decompress);
        await using (content)
        {
            return await JsonDocument.ParseAsync(content, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
