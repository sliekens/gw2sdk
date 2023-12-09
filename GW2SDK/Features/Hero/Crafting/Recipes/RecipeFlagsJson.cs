using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class RecipeFlagsJson
{
    public static RecipeFlags GetRecipeFlags(this JsonElement json)
    {
        var autoLearned = false;
        var learnedFromItem = false;
        List<string>? others = null;
        foreach (var entry in json.EnumerateArray())
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
                others ??= new List<string>();
                others.Add(entry.GetStringRequired());
            }
        }

        return new RecipeFlags
        {
            AutoLearned = autoLearned,
            LearnedFromItem = learnedFromItem,
            Other = others ?? Empty.ListOfString
        };
    }
}
