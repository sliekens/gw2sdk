namespace GuildWars2.Hero.Crafting;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Recipe
{
    public required int Id { get; init; }

    public required int OutputItemId { get; init; }

    public required int OutputItemCount { get; init; }

    public required int MinRating { get; init; }

    public required TimeSpan TimeToCraft { get; init; }

    public required IReadOnlyCollection<CraftingDisciplineName> Disciplines { get; init; }

    public required IReadOnlyCollection<RecipeFlag> Flags { get; init; }

    public required IReadOnlyCollection<Ingredient> Ingredients { get; init; }

    /// <summary>The chat code of the recipe. This can be used to link the recipe in the chat, but also in guild or squad
    /// messages.</summary>
    public required string ChatLink { get; init; }
}
