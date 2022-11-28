using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record CraftingRecipeUnlocker : Unlocker
{
    public required int RecipeId { get; init; }

    public required IReadOnlyCollection<int>? ExtraRecipeIds { get; init; }
}
