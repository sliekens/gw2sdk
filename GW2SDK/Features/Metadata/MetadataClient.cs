using System.Diagnostics.CodeAnalysis;
using GuildWars2.Metadata.Http;

namespace GuildWars2.Metadata;

/// <summary>Provides query methods for game metadata and API metadata.</summary>
[PublicAPI]
public sealed class MetadataClient
{
    private readonly HttpClient httpClient;

    public MetadataClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(ApiVersion Value, MessageContext Context)> GetApiVersion(
        string version = "v2",
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ApiVersionRequest request = new(version) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(httpClient, cancellationToken);
    }

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Public API")]
    public Task<(Build Value, MessageContext Context)> GetBuild(
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

        return request.SendAsync(httpClient, cancellationToken);
    }
}
