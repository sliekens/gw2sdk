using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts;

[PublicAPI]
public sealed class MountNamesRequest : IHttpRequest<IReplicaSet<MountName>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/types")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<MountName>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response =
            await httpClient.SendAsync(Template, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => entry.GetMountName(MissingMemberBehavior));
        return new ReplicaSet<MountName>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
