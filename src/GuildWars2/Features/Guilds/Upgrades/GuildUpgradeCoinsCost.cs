using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a coins cost for a guild upgrade.</summary>
[DataTransferObject]
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record GuildUpgradeCoinsCost : GuildUpgradeCost
{
    /// <summary>The amount of coins required to purchase the upgrade.</summary>
    public required Coin Coins { get; init; }
}
