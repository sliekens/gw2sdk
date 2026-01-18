using System.Globalization;
using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Outfits;

/// <summary>Provides query methods for outfits and outfits unlocked on the account.</summary>
public sealed class OutfitsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="OutfitsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public OutfitsClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account/outfits

    /// <summary>Retrieves the IDs of outfits unlocked on the account associated with the access token. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetUnlockedOutfits(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/account/outfits", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            IImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    #endregion v2/account/outfits

    #region v2/outfits

    /// <summary>Retrieves all outfits.</summary>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Outfit> Value, MessageContext Context)> GetOutfits(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/outfits");
        requestBuilder.Query.AddAllIds();
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Outfit> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetOutfit());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all outfits.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<int> Value, MessageContext Context)> GetOutfitsIndex(
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/outfits");
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            IImmutableValueSet<int> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an outfit by its ID.</summary>
    /// <param name="outfitId">The outfit ID.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(Outfit Value, MessageContext Context)> GetOutfitById(
        int outfitId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/outfits");
        requestBuilder.Query.AddId(outfitId);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            Outfit value = response.Json.RootElement.GetOutfit();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves outfits by their IDs.</summary>
    /// <param name="outfitIds">The outfit IDs.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Outfit> Value, MessageContext Context)> GetOutfitsByIds(
        IEnumerable<int> outfitIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/outfits");
        requestBuilder.Query.AddIds(
            outfitIds.Select(id => id.ToString(CultureInfo.InvariantCulture))
        );
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Outfit> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetOutfit());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of outfits.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="language">The language to use for descriptions.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IImmutableValueSet<Outfit> Value, MessageContext Context)> GetOutfitsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/outfits");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        requestBuilder.Query.AddLanguage(language);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            IImmutableValueSet<Outfit> value = response.Json.RootElement.GetSet(static (in entry) => entry.GetOutfit());
            return (value, response.Context);
        }
    }

    #endregion v2/outfits
}
