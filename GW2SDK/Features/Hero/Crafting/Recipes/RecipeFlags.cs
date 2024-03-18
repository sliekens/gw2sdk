namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Modifiers for crafting recipes.</summary>
[PublicAPI]
public sealed record RecipeFlags : Flags
{
    /// <summary>Whether the recipe is automatically learned when the player reaches the required discipline level.</summary>
    public required bool AutoLearned { get; init; }

    /// <summary>Whether the recipe is learned from a recipe sheet.</summary>
    public required bool LearnedFromItem { get; init; }
}
