using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class RecipeFlagsJson
{
    public static RecipeFlags GetRecipeFlags(this in JsonElement json)
    {
        bool autoLearned = false;
        bool learnedFromItem = false;
        ImmutableList<string>.Builder others = ImmutableList.CreateBuilder<string>();
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("AutoLearned"))
            {
                autoLearned = true;
            }
            else if (entry.ValueEquals("LearnedFromItem"))
            {
                learnedFromItem = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new RecipeFlags
        {
            AutoLearned = autoLearned,
            LearnedFromItem = learnedFromItem,
            Other = new ImmutableValueList<string>(others.ToImmutable())
        };
    }
}
