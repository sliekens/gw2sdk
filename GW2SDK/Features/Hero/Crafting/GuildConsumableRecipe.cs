namespace GuildWars2.Hero.Crafting;

[PublicAPI]
public sealed record GuildConsumableRecipe : Recipe
{
    public required IReadOnlyCollection<GuildIngredient>? GuildIngredients { get; init; }

    public required int OutputUpgradeId { get; init; }
}
