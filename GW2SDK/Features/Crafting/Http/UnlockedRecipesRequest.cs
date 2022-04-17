using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class UnlockedRecipesRequest : IHttpRequest<IReplica<IReadOnlyCollection<int>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/account/recipes")
    {
        AcceptEncoding = "gzip"
    };

    public string? AccessToken { get; init; }

    public async Task<IReplica<IReadOnlyCollection<int>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        var request = Template with
        {
            Arguments = search,
            BearerToken = AccessToken
        };

        using var response = await httpClient.SendAsync(request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetInt32());
        return new Replica<IReadOnlyCollection<int>>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
