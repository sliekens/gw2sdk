﻿using GuildWars2.Equipment.Http;

namespace GuildWars2.Equipment;

/// <summary>Query methods for items equipped by a character and legendary items on the account.</summary>
[PublicAPI]
public sealed class EquipmentQuery
{
    private readonly HttpClient http;

    public EquipmentQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/equipment

    /// <summary>Retrieves the equipment of a character on the account. This endpoint is only accessible with a valid access
    /// token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<CharacterEquipment>> GetCharacterEquipment(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharacterEquipmentRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/characters/:id/equipment

    #region v2/account/legendaryarmory

    /// <summary>Retrieves the number of bound legendary items on the account. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<BoundLegendaryItem>>> GetBoundLegendaryItems(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BoundLegendaryItemsRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
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
    public Task<Replica<IReadOnlyList<int>>> GetEquipmentTemplateNumbers(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedEquipmentTabsRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves an equipment template of a character on the account. This endpoint is only accessible with a valid
    /// access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="templateNumber">The number of the template to fetch.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<EquipmentTemplate>> GetEquipmentTemplate(
        string characterName,
        int templateNumber,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EquipmentTemplateRequest request = new(characterName, templateNumber)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves all unlocked equipment templates of a character on the account. This endpoint is only accessible
    /// with a valid access token.</summary>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<EquipmentTemplate>>> GetEquipmentTemplates(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EquipmentTemplatesRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves the currently active equipment tab of a character on the account. This endpoint is only accessible
    /// with a valid access token.</summary>
    /// <remarks>Expect there to be a delay after switching tabs in the game before this is reflected in the API.</remarks>
    /// <param name="characterName">A character name that belongs to the account associated with the access token.</param>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<EquipmentTemplate>> GetActiveEquipmentTemplate(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ActiveEquipmentTemplateRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/characters/:id/equipmenttabs

    #region v2/legendaryarmory

    /// <summary>Retrieves all legendary items.</summary>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<LegendaryItem>>> GetLegendaryItems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves the IDs of all legendary items.</summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<int>>> GetLegendaryItemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves a legendary item by its ID.</summary>
    /// <param name="legendaryItemId">The item ID.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<LegendaryItem>> GetLegendaryItemById(
        int legendaryItemId,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemByIdRequest request = new(legendaryItemId)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves legendary items by their IDs.</summary>
    /// <remarks>Limited to 200 IDs per request.</remarks>
    /// <param name="legendaryItemIds">The item IDs.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<LegendaryItem>>> GetLegendaryItemsByIds(
        IReadOnlyCollection<int> legendaryItemIds,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsByIdsRequest request = new(legendaryItemIds)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    /// <summary>Retrieves a page of legendary items.</summary>
    /// <param name="pageIndex">How many pages to skip. The first page starts at 0.</param>
    /// <param name="pageSize">How many entries to take.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<Replica<HashSet<LegendaryItem>>> GetLegendaryItemsByPage(
        int pageIndex,
        int? pageSize = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/legendaryarmory
}