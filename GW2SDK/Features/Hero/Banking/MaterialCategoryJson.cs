using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class MaterialCategoryJson
{
    public static MaterialCategory GetMaterialCategory(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember items = "items";
        RequiredMember order = "order";

        foreach (JsonProperty member in json.EnumerateObject())
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
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Items = items.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetInt32())),
            Order = order.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
