using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Meta;

[PublicAPI]
public sealed class MetaQuery
{
    private readonly HttpClient http;

    public MetaQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<IReplica<ApiVersion>> GetApiVersion(
        string version = "v2",
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ApiVersionRequest request = new(version) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Public API")]
    public Task<IReplica<Build>> GetBuild(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // The public API doesn't work right
        //var request = new BuildRequest { MissingMemberBehavior = missingMemberBehavior };

        // Use this private API
        var request = new AssetCdnBuildRequest();

        return request.SendAsync(http, cancellationToken);
    }
}
