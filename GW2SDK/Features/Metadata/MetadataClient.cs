using System.Diagnostics.CodeAnalysis;
using GuildWars2.Json;
using GuildWars2.Metadata.Http;

namespace GuildWars2.Metadata;

/// <summary>Provides query methods for game metadata and API metadata.</summary>
[PublicAPI]
public sealed class MetadataClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="MetadataClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public MetadataClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves information about an API version, either v1 or v2.</summary>
    /// <param name="version">The API version.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(ApiVersion Value, MessageContext Context)> GetApiVersion(
        string version = "v2",
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        JsonOptions.MissingMemberBehavior = missingMemberBehavior;
        ApiVersionRequest request = new(version);
        return request.SendAsync(httpClient, cancellationToken);
    }

    /// <summary>Retrieves the latest build ID of the game client.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
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
