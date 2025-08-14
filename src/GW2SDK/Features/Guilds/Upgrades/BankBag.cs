namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about an upgrade that increases the guild vault's capacity.</summary>
public sealed record BankBag : GuildUpgrade
{
    /// <summary>The number of item slots that will be added to the guild vault.</summary>
    public required int MaxItems { get; init; }

    /// <summary>The number of coins that can be stored in this section of the guild vault.</summary>
    public required Coin MaxCoins { get; init; }
}
