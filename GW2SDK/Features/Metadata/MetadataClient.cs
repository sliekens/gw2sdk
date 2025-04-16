using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using GuildWars2.Http;
using GuildWars2.Json;

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
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves information about an API version, either v1 or v2.</summary>
    /// <param name="version">The API version.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(ApiVersion Value, MessageContext Context)> GetApiVersion(
        string version = "v2",
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet($"{version}.json");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetApiVersion();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the latest build ID of the game client.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Build Value, MessageContext Context)> GetBuild(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/build");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetBuild();
            return (value, response.Context);
        }
    }
}
