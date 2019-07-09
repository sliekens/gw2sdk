using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public enum RecipeFlag
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(RecipeFlag)
        AutoLearned = 1,

        LearnedFromItem
    }
}
