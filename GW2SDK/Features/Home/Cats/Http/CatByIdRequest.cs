using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Home.Cats.Json;
using GW2SDK.Home.Cats.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Home.Cats.Http;

[PublicAPI]
public sealed class CatByIdRequest : IHttpRequest<IReplica<Cat>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "/v2/home/cats") { AcceptEncoding = "gzip" };

    public CatByIdRequest(int catId)
    {
        CatId = catId;
    }

    public int CatId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Cat>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", CatId);
        var request = Template with { Arguments = search };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = CatReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Cat>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
