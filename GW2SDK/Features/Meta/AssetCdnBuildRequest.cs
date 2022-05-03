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
        var latest64 = await httpClient
            .GetStringAsync("http://assetcdn.101.ArenaNetworks.com/latest64/101")
            .ConfigureAwait(false);

        if (latest64 is null)
        {
            throw new InvalidOperationException("Missing value.");
        }

        var text = latest64[..latest64.IndexOf(' ')];
        return new Replica<Build>(
            DateTimeOffset.UtcNow,
            new Build { Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo) }
        );
    }
}
