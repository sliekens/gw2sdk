using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record CraftingRecipeUnlocker : Unlocker
    {
        public int RecipeId { get; init; }

        public int[]? ExtraRecipeIds { get; init; }
    }
}
