using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Mounts.Json;
using GW2SDK.Mounts.Models;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountByNameRequest : IHttpRequest<IReplica<Mount>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/types")
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

    public async Task<IReplica<Mount>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        QueryBuilder search = new();
        search.Add("id", MountNameFormatter.FormatMountName(MountName));
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = MountReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Mount>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
