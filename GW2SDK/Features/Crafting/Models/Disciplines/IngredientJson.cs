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
        RequiredMember type = new("type");
        RequiredMember id = new("id");
        RequiredMember count = new("count");
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
            Kind = type.Select(value => value.GetEnum<IngredientKind>(missingMemberBehavior)),
            Id = id.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}
