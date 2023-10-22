namespace GuildWars2.Crafting;

[PublicAPI]
public sealed record GuildDecorationRecipe : Recipe
{
    public required IReadOnlyCollection<GuildIngredient>? GuildIngredients { get; init; }

    public required int OutputUpgradeId { get; init; }
}
