using GuildWars2.Armory.Http;

namespace GuildWars2.Armory;

[PublicAPI]
public sealed class ArmoryQuery
{
    private readonly HttpClient http;

    public ArmoryQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/equipment

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

    public Task<Replica<HashSet<BoundLegendaryItem>>> GetBoundLegendaryItems(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BoundLegendaryItemsRequest request =
            new(accessToken) { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/account/legendaryarmory

    #region v2/characters/:id/equipmenttabs

    public Task<Replica<IReadOnlyList<int>>> GetUnlockedEquipmentTabs(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedEquipmentTabsRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<EquipmentTab>> GetEquipmentTab(
        string characterName,
        int tab,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        EquipmentTabRequest request = new(characterName, tab)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<EquipmentTab>>> GetEquipmentTabs(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        EquipmentTabsRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<EquipmentTab>> GetActiveEquipmentTab(
        string characterName,
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        ActiveEquipmentTabRequest request = new(characterName) { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion v2/characters/:id/equipmenttabs

    #region v2/legendaryarmory

    public Task<Replica<HashSet<LegendaryItem>>> GetLegendaryItems(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetLegendaryItemsIndex(
        CancellationToken cancellationToken = default
    )
    {
        LegendaryItemsIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

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
