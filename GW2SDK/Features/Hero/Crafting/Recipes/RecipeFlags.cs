namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Modifiers for crafting recipes.</summary>
[PublicAPI]
public sealed record RecipeFlags
{
    /// <summary>Whether the recipe is automatically learned when the player reaches the required discipline level.</summary>
    public required bool AutoLearned { get; init; }

    /// <summary>Whether the recipe is learned from a recipe sheet.</summary>
    public required bool LearnedFromItem { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
