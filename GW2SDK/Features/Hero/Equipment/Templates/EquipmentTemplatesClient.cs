using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>Provides query methods for items equipped by a character and legendary items on the account.</summary>
[PublicAPI]
public sealed class EquipmentTemplatesClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="EquipmentTemplatesClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public EquipmentTemplatesClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/equipment

    /// <summary>Retrieves the equipment of a character on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(CharacterEquipment Value, MessageContext Context)> GetCharacterEquipment(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet($"v2/characters/{characterName}/equipment", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetCharacterEquipment();
            return (value, response.Context);
        }
    }

    #endregion v2/characters/:id/equipment

    #region v2/account/legendaryarmory

    /// <summary>Retrieves the number of bound legendary items on the account. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<BoundLegendaryItem> Value, MessageContext Context)>
        GetBoundLegendaryItems(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/account/legendaryarmory", accessToken);
        requestBuilder.Query.AddAllIds();
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetBoundLegendaryItem());
            return (value, response.Context);
        }
    }

    #endregion v2/account/legendaryarmory

    #region v2/characters/:id/equipmenttabs

    /// <summary>Retrieves the unlocked equipment template numbers of a character on the account. Each character has 2
    /// templates unlocked initially, which can be expanded up to 8 with Equipment Template Expansions. This endpoint is only
    /// accessible with a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(IReadOnlyList<int> Value, MessageContext Context)>
        GetEquipmentTemplateNumbers(
            string characterName,
            string? accessToken,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet($"v2/characters/{characterName}/equipmenttabs", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetList(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves an equipment template of a character on the account. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="templateNumber">The number of the template to fetch.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(EquipmentTemplate Value, MessageContext Context)> GetEquipmentTemplate(
        string characterName,
        int templateNumber,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet($"v2/characters/{characterName}/equipmenttabs/{templateNumber}", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetEquipmentTemplate();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves all unlocked equipment templates of a character on the account. This endpoint is only accessible
    /// with a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<EquipmentTemplate> Value, MessageContext Context)>
        GetEquipmentTemplates(
            string characterName,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        // There is no ids=all support, but page=0 works
        var requestBuilder = RequestBuilder.HttpGet($"v2/characters/{characterName}/equipmenttabs", accessToken);
        requestBuilder.Query.AddPage(0, null);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetEquipmentTemplate());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the currently active equipment tab of a character on the account. This endpoint is only accessible
    /// with a valid access token.</summary>
    /// <remarks>Expect there to be a delay after switching tabs in the game before this is reflected in the API.</remarks>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(EquipmentTemplate Value, MessageContext Context)> GetActiveEquipmentTemplate(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet($"v2/characters/{characterName}/equipmenttabs/active", accessToken);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetEquipmentTemplate();
            return (value, response.Context);
        }
    }

    #endregion v2/characters/:id/equipmenttabs

    #region v2/legendaryarmory

    /// <summary>Retrieves all legendary items.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<LegendaryItem> Value, MessageContext Context)> GetLegendaryItems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/legendaryarmory");
        requestBuilder.Query.AddAllIds();
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetLegendaryItem());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves the IDs of all legendary items.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<int> Value, MessageContext Context)> GetLegendaryItemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/legendaryarmory");
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            var value = response.Json.RootElement.GetSet(static entry => entry.GetInt32());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a legendary item by its ID.</summary>
    /// <param name="legendaryItemId">The item ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(LegendaryItem Value, MessageContext Context)> GetLegendaryItemById(
        int legendaryItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/legendaryarmory");
        requestBuilder.Query.AddId(legendaryItemId);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetLegendaryItem();
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves legendary items by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="legendaryItemIds">The item IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<LegendaryItem> Value, MessageContext Context)>
        GetLegendaryItemsByIds(
            IEnumerable<int> legendaryItemIds,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/legendaryarmory");
        requestBuilder.Query.AddIds(legendaryItemIds);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetLegendaryItem());
            return (value, response.Context);
        }
    }

    /// <summary>Retrieves a page of legendary items.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(HashSet<LegendaryItem> Value, MessageContext Context)>
        GetLegendaryItemsByPage(
            int pageIndex,
            int? pageSize = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
    {
        var requestBuilder = RequestBuilder.HttpGet("v2/legendaryarmory");
        requestBuilder.Query.AddPage(pageIndex, pageSize);
        var request = requestBuilder.Build();
        var response = await httpClient.AcceptJsonAsync(request, cancellationToken).ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            var value = response.Json.RootElement.GetSet(static entry => entry.GetLegendaryItem());
            return (value, response.Context);
        }
    }

    #endregion v2/legendaryarmory
}
