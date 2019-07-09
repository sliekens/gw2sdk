using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class GuildConsumableRecipe : Recipe
    {
        [CanBeNull]
        public GuildIngredient[] GuildIngredients { get; set; }

        public int OutputUpgradeId { get; set; }
    }
}
