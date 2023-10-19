using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class IngredientJson
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
            if (member.NameEquals(type.Name))
            {
                type = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(count.Name))
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
            Kind = type.Select(value => value.GetEnum<IngredientKind>(missingMemberBehavior)),
            Id = id.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
