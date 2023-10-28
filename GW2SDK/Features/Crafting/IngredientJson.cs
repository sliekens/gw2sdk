using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

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
            if (member.Name == type.Name)
            {
                type = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == count.Name)
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
