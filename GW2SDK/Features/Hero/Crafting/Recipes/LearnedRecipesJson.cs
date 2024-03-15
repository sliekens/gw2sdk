using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class LearnedRecipesJson
{
    public static HashSet<int> GetLearnedRecipes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember recipes = "recipes";

        foreach (var member in json.EnumerateObject())
        {
            if (recipes.Match(member))
            {
                recipes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return [.. recipes.Map(values => values.GetList(value => value.GetInt32()))];
    }
}
