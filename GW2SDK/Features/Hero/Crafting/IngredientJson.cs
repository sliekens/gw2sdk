using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal static class IngredientJson
{
    public static Ingredient GetIngredient(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Ingredient
        {
            Kind = type.Map(static value => value.GetEnum<IngredientKind>()),
            Id = id.Map(static value => value.GetInt32()),
            Count = count.Map(static value => value.GetInt32())
        };
    }
}
