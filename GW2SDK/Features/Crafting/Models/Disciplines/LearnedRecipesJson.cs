using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class LearnedRecipesJson
{
    public static HashSet<int> GetLearnedRecipes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember recipes = "recipes";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(recipes.Name))
            {
                recipes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HashSet<int>(recipes.Map(values => values.GetList(value => value.GetInt32())));
    }
}
