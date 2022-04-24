using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Meta;

[PublicAPI]
public sealed class BuildRequest : IHttpRequest<IReplica<Build>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/build");

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Build>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
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

        var value = BuildReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Build>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
