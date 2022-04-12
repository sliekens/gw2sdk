using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Builds;

[PublicAPI]
public sealed class BuildService
{
    private readonly HttpClient http;

    public BuildService(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplica<Build>> GetBuild(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // The public API doesn't work right
        //BuildRequest request = new();
        //return await http.GetResource(request,
        //        json => BuildReader.Read(json.RootElement, missingMemberBehavior),
        //        cancellationToken)
        //    .ConfigureAwait(false);

        // Use this private API
        var latest64 = await http.GetStringAsync("http://assetcdn.101.ArenaNetworks.com/latest64/101")
            .ConfigureAwait(false);

        var build = 127440; // At the time of writing
        if (latest64 is null)
        {
            return new Replica<Build>(DateTimeOffset.UtcNow,
                new Build
                {
                    Id = build
                });
        }

        var text = latest64.Substring(0, latest64.IndexOf(' '));
        return new Replica<Build>(DateTimeOffset.UtcNow,
            new Build
            {
                Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo)
            });
    }
}