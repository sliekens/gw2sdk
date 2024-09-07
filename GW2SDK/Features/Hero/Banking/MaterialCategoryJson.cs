using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class MaterialCategoryJson
{
    public static MaterialCategory GetMaterialCategory(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MaterialCategory
        {
            Id = id.Map(static value => value.GetInt32()),
            Name = name.Map(static value => value.GetStringRequired()),
            Items = items.Map(static values => values.GetList(static value => value.GetInt32())),
            Order = order.Map(static value => value.GetInt32())
        };
    }
}
