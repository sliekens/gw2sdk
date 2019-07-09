using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class CraftingRecipeUnlocker : Unlocker
    {
        public int RecipeId { get; set; }

        [CanBeNull]
        public int[] ExtraRecipeIds { get; set; }
    }
}