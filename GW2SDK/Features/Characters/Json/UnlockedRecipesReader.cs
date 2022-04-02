using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Characters.Json
{
    [PublicAPI]
    public static class UnlockedRecipesReader
    {
        public static UnlockedRecipesView Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
        {
            var recipes = new RequiredMember<int>("recipes");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(recipes.Name))
                {
                    recipes = recipes.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
                }
            }

            return new UnlockedRecipesView
            {
                Recipes = recipes.SelectMany(value => value.GetInt32())
            };
        }
    }
}
