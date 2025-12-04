namespace GuildWars2.Guilds.Treasury;

/// <summary>Information about an item in the guild treasury.</summary>
[DataTransferObject]
public sealed record GuildTreasurySlot
{
    /// <summary>The item ID.</summary>
    public required int ItemId { get; init; }

    /// <summary>How many of the item are currently stored in the treasury.</summary>
    public required int Count { get; init; }

    /// <summary>The count of this item which is needed for currently in-progress guild upgrades.</summary>
    public required IReadOnlyCollection<CountNeededForUpgrade> CountNeededForUpgrades { get; init; }
}
