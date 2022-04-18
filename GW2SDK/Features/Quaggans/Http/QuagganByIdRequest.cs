using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Quaggans.Json;
using GW2SDK.Quaggans.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Quaggans.Http;

[PublicAPI]
public sealed class QuagganByIdRequest : IHttpRequest<IReplica<Quaggan>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public QuagganByIdRequest(string quagganId)
    {
        QuagganId = quagganId;
    }

    public string QuagganId { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Quaggan>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", QuagganId);
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

        var value = QuagganReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Quaggan>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
