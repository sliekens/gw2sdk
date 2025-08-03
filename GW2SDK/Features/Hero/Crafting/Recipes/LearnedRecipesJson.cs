using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class LearnedRecipesJson
{
    public static HashSet<int> GetLearnedRecipes(this in JsonElement json)
    {
        RequiredMember recipes = "recipes";

        foreach (var member in json.EnumerateObject())
        {
            if (recipes.Match(member))
            {
                recipes = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return recipes.Map(static (in JsonElement values) => values.GetSet(static (in JsonElement value) => value.GetInt32()));
    }
}
