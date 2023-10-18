namespace GuildWars2.Guilds.Treasury;

/// <summary>An item in the guild treasury.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildTreasurySlot
{
    /// <summary>The item's ID.</summary>
    public required string ItemId { get; init; }

    /// <summary>How many of the item are currently in the treasury.</summary>
    public required int Count { get; init; }

    /// <summary>The count of this item which is needed for currently in-progress guild upgrades.</summary>

    public required IReadOnlyCollection<CountNeededForUpgrade> CountNeededForUpgrades { get; init; }
}
