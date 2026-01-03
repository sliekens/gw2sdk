namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about an item cost for a guild upgrade.</summary>
[DataTransferObject]
public sealed record GuildUpgradeItemCost : GuildUpgradeCost
{
    /// <summary>The name of the item required.</summary>
    public required string Name { get; init; }

    /// <summary>The item ID of the item required.</summary>
    public required int ItemId { get; init; }

    /// <summary>The amount of the item required.</summary>
    public required int Count { get; init; }
}
