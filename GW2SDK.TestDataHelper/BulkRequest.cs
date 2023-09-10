using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;

namespace GuildWars2.TestDataHelper;

public class BulkRequest : IHttpRequest<JsonDocument>
{
    private readonly string requestUri;

    public BulkRequest(string requestUri)
    {
        this.requestUri = requestUri;
    }

    public required IReadOnlyCollection<int> Ids { get; init; }

    public async Task<JsonDocument> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new()
        {
            { "ids", Ids },
            { "v", "3" }
        };

        var requestTemplate = new HttpRequestMessageTemplate(HttpMethod.Get, requestUri)
        {
            Arguments = search,
            AcceptEncoding = "gzip"
        };

        using var response = await httpClient.SendAsync(
                requestTemplate,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        return await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
    }
}
