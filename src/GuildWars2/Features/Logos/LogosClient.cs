using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Logos;

/// <summary>Provides query methods for logos.</summary>
public sealed class LogosClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="LogosClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public LogosClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves all logos.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Logo> Value, MessageContext Context)> GetLogos(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/logos");
        requestBuilder.Query.AddAllIds();
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<Logo> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetLogo());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all logos.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<string> Value, MessageContext Context)> GetLogosIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/logos");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ValueHashSet<string> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetStringRequired());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a logo by its ID.</summary>
    /// <param name="logoId">The logo ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Logo Value, MessageContext Context)> GetLogoById(
        string logoId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/logos");
        requestBuilder.Query.AddId(logoId);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Logo value = response.Json.RootElement.GetLogo();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves logos by their IDs.</summary>
    /// <param name="logoIds">The logo IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Logo> Value, MessageContext Context)> GetLogosByIds(
        IEnumerable<string> logoIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/logos");
        requestBuilder.Query.AddIds(logoIds);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<Logo> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetLogo());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of logos.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<Logo> Value, MessageContext Context)> GetLogosByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/logos");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ValueHashSet<Logo> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetLogo());
            return (value, response.Context);
        }
    }
}
