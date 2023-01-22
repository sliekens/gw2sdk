using System.Collections.Generic;
using JetBrains.Annotations;

namespace GuildWars2.Crafting;

[PublicAPI]
public sealed record GuildConsumableRecipe : Recipe
{
    public required IReadOnlyCollection<GuildIngredient>? GuildIngredients { get; init; }

    public required int OutputUpgradeId { get; init; }
}
