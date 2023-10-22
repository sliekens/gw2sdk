using System.Diagnostics.CodeAnalysis;
using GuildWars2.Meta.Http;

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

    public Task<Replica<ApiVersion>> GetApiVersion(
        string version = "v2",
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ApiVersionRequest request = new(version) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Public API")]
    public Task<Replica<Build>> GetBuild(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // The API has been stuck on build 115267 since at least 2021-05-27
        //var request = new BuildRequest { MissingMemberBehavior = missingMemberBehavior };

        // A undocumented API is used to find the current build
        // The same API is used by the Guild Wars 2 launcher to check for updates
        // So in a sense, this is the "official" way to find the current build
        var request = new AssetCdnBuildRequest();

        return request.SendAsync(http, cancellationToken);
    }
}
