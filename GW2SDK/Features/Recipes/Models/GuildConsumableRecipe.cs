using JetBrains.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed record GuildConsumableRecipe : Recipe
    {
        public GuildIngredient[]? GuildIngredients { get; init; }
        
        public int OutputUpgradeId { get; init; }
    }
}
