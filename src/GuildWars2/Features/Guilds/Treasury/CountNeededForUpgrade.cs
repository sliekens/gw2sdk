namespace GuildWars2.Guilds.Treasury;

/// <summary>Information about an item needed for a guild upgrade.</summary>
[DataTransferObject]
public sealed record CountNeededForUpgrade
{
    /// <summary>The ID of the guild upgrade which needs the item.</summary>
    public required int UpgradeId { get; init; }

    /// <summary>The total amount of the item needed for the upgrade.</summary>
    public required int Count { get; init; }
}
