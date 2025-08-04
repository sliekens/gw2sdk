namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild collectible cost for a guild upgrade, for example Favor.</summary>
[DataTransferObject]
public sealed record GuildUpgradeCollectibleCost : GuildUpgradeCost
{
    /// <summary>The name of the collectible required.</summary>
    public required string Name { get; init; }

    /// <summary>The item ID of the collectible required.</summary>
    public required int ItemId { get; init; }

    /// <summary>The amount of the collectible required.</summary>
    public required int Count { get; init; }
}
