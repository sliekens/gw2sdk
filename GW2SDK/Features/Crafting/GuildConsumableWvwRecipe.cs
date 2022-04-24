using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
public sealed record GuildConsumableWvwRecipe : Recipe
{
    public int? OutputUpgradeId { get; init; }
}
