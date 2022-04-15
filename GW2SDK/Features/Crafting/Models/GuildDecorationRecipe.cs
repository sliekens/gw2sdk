using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Crafting.Models;

[PublicAPI]
public sealed record GuildDecorationRecipe : Recipe
{
    public IReadOnlyCollection<GuildIngredient>? GuildIngredients { get; init; }

    public int OutputUpgradeId { get; init; }
}
