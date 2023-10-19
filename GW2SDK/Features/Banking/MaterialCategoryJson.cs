using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Banking;

[PublicAPI]
public static class MaterialCategoryJson
{
    public static MaterialCategory GetMaterialCategory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember items = new("items");
        RequiredMember order = new("order");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(items.Name))
            {
                items.Value = member.Value;
            }
            else if (member.NameEquals(order.Name))
            {
                order.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MaterialCategory
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Items = items.SelectMany(value => value.GetInt32()),
            Order = order.Select(value => value.GetInt32())
        };
    }
}
