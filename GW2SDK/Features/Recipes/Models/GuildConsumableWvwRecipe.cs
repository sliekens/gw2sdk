using JetBrains.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed record GuildConsumableWvwRecipe : Recipe
    {
        public int? OutputUpgradeId { get; init; }
    }
}
