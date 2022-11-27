using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
public sealed record GuildConsumableWvwRecipe : Recipe
{
    public required int? OutputUpgradeId { get; init; }
}
