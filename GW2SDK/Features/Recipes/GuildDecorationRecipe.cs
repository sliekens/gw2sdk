using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Recipes
{
    [PublicAPI]
    public sealed class GuildDecorationRecipe : Recipe
    {
        [CanBeNull]
        public GuildIngredient[] GuildIngredients { get; set; }

        public int OutputUpgradeId { get; set; }
    }
}
