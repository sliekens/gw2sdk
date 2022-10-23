using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Meta;

[PublicAPI]
public sealed class BuildRequest : IHttpRequest<IReplica<Build>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/build")
    {
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Build>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response =
            await httpClient.SendAsync(Template, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetBuild(MissingMemberBehavior);
        return new Replica<Build>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
