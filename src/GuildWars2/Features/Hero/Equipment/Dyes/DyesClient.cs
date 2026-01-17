using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>Provides query methods for dye colors and unlocked dyes.</summary>
public sealed class DyesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="DyesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public DyesClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/dyes

    /// <summary>Retrieves the color IDs of dyes unlocked on the account associated with the access token. This endpoint is
    /// only accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetUnlockedColors(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/dyes", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/account/dyes

    #region v2/colors

    /// <summary>Retrieves all dye colors.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<DyeColor> Value, MessageContext Context)> GetColors(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/colors");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<DyeColor> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetDyeColor());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all dye colors.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetColorsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/colors");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            ImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a dye color by its ID.</summary>
    /// <param name="colorId">The dye color ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(DyeColor Value, MessageContext Context)> GetColorById(
        int colorId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/colors");
        requestBuilder.Query.AddId(colorId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            DyeColor value = response.Json.RootElement.GetDyeColor();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves dye colors by their IDs.</summary>
    /// <param name="colorIds">The dye color IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<DyeColor> Value, MessageContext Context)> GetColorsByIds(
        IEnumerable<int> colorIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/colors");
        requestBuilder.Query.AddIds(colorIds);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<DyeColor> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetDyeColor());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of dye colors.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<DyeColor> Value, MessageContext Context)> GetColorsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/colors");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            ImmutableValueSet<DyeColor> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetDyeColor());
            return (value, response.Context);
        }
    }

    #endregion v2/colors
}
