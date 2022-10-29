using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts;

[PublicAPI]
public sealed class MountByNameRequest : IHttpRequest<IReplica<Mount>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountByNameRequest(MountName mountName)
    {
        Check.Constant(mountName, nameof(mountName));
        MountName = mountName;
    }

    public MountName MountName { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Mount>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments =
                    new QueryBuilder
                    {
                        { "id", MountNameFormatter.FormatMountName(MountName) },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetMount(MissingMemberBehavior);
        return new Replica<Mount>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
