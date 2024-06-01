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
        requestBuilder.Query.AddSchemaVersion(SchemaVersion.Recommended);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
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
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Public API")]
    public async Task<(Build Value, MessageContext Context)> GetBuild(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        // The /v2/build API has been stuck on build 115267 since at least 2021-05-27
        // A undocumented API is used to find the current build
        // The same API is used by the Guild Wars 2 launcher to check for updates
        // So in a sense, this is the "official" way to find the current build
        var (value, context) = await Latest("http://assetcdn.101.ArenaNetworks.com/latest/101")
            .ConfigureAwait(false);
        if (value is null)
        {
            (value, context) = await Latest("http://assetcdn.101.ArenaNetworks.com/latest64/101")
                .ConfigureAwait(false);
        }

        return (value ?? throw new InvalidOperationException("Missing value."), context);

        async Task<(Build? Value, MessageContext Context)> Latest(string url)
        {
            using var request = new HttpRequestMessage(Get, url);
            using var response = await httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken
                )
                .ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return (null, new MessageContext(response));
            }

#if NET
            var latest = await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            var latest = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
            var text = latest[..latest.IndexOf(' ')];
            var build = new Build
            {
                Id = int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo)
            };
            return (build, new MessageContext(response));
        }
    }
}
