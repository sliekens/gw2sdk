using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class GuildDecorationRecipe : Recipe
    {
        [CanBeNull]
        public GuildIngredient[] GuildIngredients { get; set; }

        public int OutputUpgradeId { get; set; }
    }
}
