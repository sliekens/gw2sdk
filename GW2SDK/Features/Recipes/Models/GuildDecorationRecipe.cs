using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed record GuildDecorationRecipe : Recipe
    {
        public GuildIngredient[]? GuildIngredients { get; init; }
        
        public int OutputUpgradeId { get; init; }
    }
}
