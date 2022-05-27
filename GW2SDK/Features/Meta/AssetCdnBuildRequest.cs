using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
public sealed class AssetCdnBuildRequest : IHttpRequest<IReplica<Build>>
{
    public async Task<IReplica<Build>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        const string resource = "http://assetcdn.101.ArenaNetworks.com/latest64/101";
        var latest64 = await httpClient
#if NET
            .GetStringAsync(resource, cancellationToken)
#else
            .GetStringAsync(resource)
#endif
            .ConfigureAwait(false);

        if (latest64 is null)
        {
            throw new InvalidOperationException("Missing value.");
        }

        var text = latest64.Substring(0, latest64.IndexOf(' '));
        return new Replica<Build>(
            DateTimeOffset.UtcNow,
            new Build { Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo) }
        );
    }
}
