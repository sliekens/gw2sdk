namespace GuildWars2.Hero.Crafting;

[PublicAPI]
public sealed record GuildConsumableWvwRecipe : Recipe
{
    public required int? OutputUpgradeId { get; init; }
}
