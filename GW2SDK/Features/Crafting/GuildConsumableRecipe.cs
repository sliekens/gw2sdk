using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
public sealed record GuildConsumableRecipe : Recipe
{
    public required IReadOnlyCollection<GuildIngredient>? GuildIngredients { get; init; }

    public required int OutputUpgradeId { get; init; }
}
