using System.Collections.Generic;

using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record CraftingRecipeUnlocker : Unlocker
{
    public int RecipeId { get; init; }

    public IReadOnlyCollection<int>? ExtraRecipeIds { get; init; }
}