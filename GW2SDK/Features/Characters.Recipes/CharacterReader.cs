using System;
using System.Text.Json;
using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Characters.Recipes
{
    [PublicAPI]
    public sealed class CharacterReader : ICharacterReader
    {
        public Character Read(JsonElement json, MissingMemberBehavior missingMemberBehavior = default)
        {
            var recipes = new RequiredMember<int[]>("recipes");

            foreach (var member in json.EnumerateObject())
            {
                if (member.NameEquals(recipes.Name))
                {
                    recipes = recipes.From(member.Value);
                }
                else if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException($"Unexpected member '{member.Name}'.");
                }
            }

            return new Character { Recipes = recipes.Select(value => value.GetArray(item => item.GetInt32())) };
        }
    }
}
