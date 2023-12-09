namespace GuildWars2.Hero.Crafting;

/// <summary>Information about a guild consumable or decoration ingredient required to craft a guild upgrade.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildIngredient
{
    /// <summary>The guild upgrade ID of the consumable or decoration.</summary>
    public required int UpgradeId { get; init; }

    /// <summary>The amount of the ingredient required to craft the guild upgrade.</summary>
    public required int Count { get; init; }
}
