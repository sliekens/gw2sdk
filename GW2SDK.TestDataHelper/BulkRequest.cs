using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper;

public class BulkRequest : IHttpRequest<JsonDocument>
{
    private readonly bool all;

    private readonly IReadOnlyCollection<int>? ids;

    private readonly string requestUri;

    public BulkRequest(string requestUri)
    {
        this.requestUri = requestUri;
        all = true;
    }

    public BulkRequest(string requestUri, IReadOnlyCollection<int> ids)
    {
        this.requestUri = requestUri;
        this.ids = ids;
    }

    public async Task<JsonDocument> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        if (all)
        {
            search.Add("ids", "all");
        }
        else
        {
            search.Add("ids", ids!);
        }

        var request = new HttpRequestMessageTemplate(HttpMethod.Get, requestUri)
        {
            Arguments = search,
            AcceptEncoding = "gzip"
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        return await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
    }
}
