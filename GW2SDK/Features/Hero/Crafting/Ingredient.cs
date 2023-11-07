namespace GuildWars2.Hero.Crafting;

[PublicAPI]
[DataTransferObject]
public sealed record Ingredient
{
    public required IngredientKind Kind { get; init; }

    public required int Id { get; init; }

    public required int Count { get; init; }
}
