using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class LearnedRecipesJson
{
    public static HashSet<int> GetLearnedRecipes(this in JsonElement json)
    {
        RequiredMember recipes = "recipes";

        foreach (JsonProperty member in json.EnumerateObject())
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

        return recipes.Map(static (in values) => values.GetSet(static (in value) => value.GetInt32()));
    }
}
