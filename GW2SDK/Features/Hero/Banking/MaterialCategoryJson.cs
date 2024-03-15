using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class MaterialCategoryJson
{
    public static MaterialCategory GetMaterialCategory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember items = "items";
        RequiredMember order = "order";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (items.Match(member))
            {
                items = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MaterialCategory
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Items = items.Map(values => values.GetList(value => value.GetInt32())),
            Order = order.Map(value => value.GetInt32())
        };
    }
}
