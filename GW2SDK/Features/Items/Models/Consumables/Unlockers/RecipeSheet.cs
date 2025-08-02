using System.Text.Json.Serialization;

using GuildWars2.Chat;

namespace GuildWars2.Items;

/// <summary>Information about a recipe sheet which unlocks a recipe and possibly extra recipes when consumed.</summary>
[PublicAPI]
[JsonConverter(typeof(RecipeSheetJsonConverter))]
public sealed record RecipeSheet : Unlocker
{
    /// <summary>The ID of the recipe that is unlocked when the item is consumed.</summary>
    public required int RecipeId { get; init; }

    /// <summary>The IDs of any extra recipes that are unlocked when the item is consumed.</summary>
    /// <remarks>This property is used in the following scenarios: 1. For recipes that involve multiple crafting steps, to
    /// unlock recipes for intermediary crafting materials. 2. For rune and sigil recipes, to unlock minor, major and superior
    /// variants all at once. 3. Some recipe sheets unlock variants for multiple disciplines. For instance, some armor recipe
    /// sheets may unlock a recipe for Armorsmith, Leatherworker and Tailor all at once, to produce heavy, medium or light
    /// variants.</remarks>
    public required IReadOnlyCollection<int> ExtraRecipeIds { get; init; }

    /// <summary>Gets a chat link object for this recipe.</summary>
    /// <returns>The chat link as an object.</returns>
    public RecipeLink GetRecipeChatLink()
    {
        return new() { RecipeId = RecipeId };
    }

    /// <summary>Gets chat link objects for the extra recipes.</summary>
    /// <returns>The chat links as objects.</returns>
    public IEnumerable<RecipeLink> GetExtraRecipeChatLinks()
    {
        return ExtraRecipeIds.Select(recipeId => new RecipeLink { RecipeId = recipeId });
    }
}
