using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Recipes
{
    [PublicAPI]
    public enum RecipeFlag
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(RecipeFlag)
        AutoLearned = 1,

        LearnedFromItem
    }
}
