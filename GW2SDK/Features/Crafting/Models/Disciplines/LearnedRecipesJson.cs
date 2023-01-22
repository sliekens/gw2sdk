using System;
using System.Collections.Generic;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class LearnedRecipesJson
{
    public static HashSet<int> GetLearnedRecipes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> recipes = new("recipes");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(recipes.Name))
            {
                recipes.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HashSet<int>(recipes.SelectMany(value => value.GetInt32()));
    }
}
