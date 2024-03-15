using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal static class IngredientJson
{
    public static Ingredient GetIngredient(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember type = "type";
        RequiredMember id = "id";
        RequiredMember count = "count";
        foreach (var member in json.EnumerateObject())
        {
            if (type.Match(member))
            {
                type = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Ingredient
        {
            Kind = type.Map(value => value.GetEnum<IngredientKind>(missingMemberBehavior)),
            Id = id.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}
