namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild currency cost for a guild upgrade, for example Aetherium.</summary>
[DataTransferObject]
public sealed record GuildUpgradeCurrencyCost : GuildUpgradeCost
{
    /// <summary>The name of the currency required.</summary>
    public required string Name { get; init; }

    /// <summary>The amount of currency required.</summary>
    public required int Count { get; init; }
}
