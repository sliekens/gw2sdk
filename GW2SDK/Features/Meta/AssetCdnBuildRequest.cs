using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;

namespace GuildWars2.Meta;

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
        var value = new Build
        {
            Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo)
        };
        return new Replica<Build>
        {
            Value = value,
            Date = DateTimeOffset.UtcNow,
            Expires = null,
            LastModified = null
        };
    }
}
