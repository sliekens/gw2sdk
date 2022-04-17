using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Meta.Http;
using GW2SDK.Meta.Models;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
public sealed class MetaQuery
{
    private readonly HttpClient http;

    public MetaQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplica<ApiVersion>> GetApiVersion(
        string version = "v2",
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ApiVersionRequest request = new(version)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<Build>> GetBuild(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // The public API doesn't work right
        //BuildRequest request = new();

        // Use this private API
        AssetCdnBuildRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }
}
