﻿using GuildWars2.Chat;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record RecipeSheet : Unlocker
{
    public required int RecipeId { get; init; }

    public required IReadOnlyCollection<int> ExtraRecipeIds { get; init; }

    /// <summary>Gets a chat link object for this recipe.</summary>
    /// <returns>The chat link as an object.</returns>
    public RecipeLink GetRecipeChatLink() => new() { RecipeId = RecipeId };

    /// <summary>Gets chat link objects for the extra recipes.</summary>
    /// <returns>The chat links as objects.</returns>
    public IEnumerable<RecipeLink> GetExtraRecipeChatLinks() =>
        ExtraRecipeIds.Select(recipeId => new RecipeLink { RecipeId = recipeId });
}
