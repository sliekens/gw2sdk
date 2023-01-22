using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class IngredientJson
{
    public static Ingredient GetIngredient(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<IngredientKind> type = new("type");
        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Ingredient
        {
            Kind = type.GetValue(missingMemberBehavior),
            Id = id.GetValue(),
            Count = count.GetValue()
        };
    }
}
