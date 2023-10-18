namespace GuildWars2.Guilds.Treasury;

[PublicAPI]
[DataTransferObject]
public sealed record CountNeededForUpgrade

{
    /// <summary>The ID of the upgrade needing the item.</summary>
    public required int UpgradeId { get; init; }

    /// <summary>The total amount of the item needed for this upgrade.</summary>
    public required int Count { get; init; }
}
