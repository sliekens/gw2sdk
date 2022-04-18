using System.Collections.Generic;
using System.Linq;
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
public sealed class MountsByNamesRequest : IHttpRequest<IReplicaSet<Mount>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountsByNamesRequest(IReadOnlyCollection<MountName> mountNames)
    {
        Check.Collection(mountNames, nameof(mountNames));
        MountNames = mountNames;
    }

    public IReadOnlyCollection<MountName> MountNames { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<Mount>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("ids", MountNames.Select(name => MountNameFormatter.FormatMountName(name)));
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value =
            json.RootElement.GetSet(entry => MountReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<Mount>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
