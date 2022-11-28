using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Banking;

[PublicAPI]
public static class MaterialCategoryJson
{
    public static MaterialCategory GetMaterialCategory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> items = new("items");
        RequiredMember<int> order = new("order");

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
            Id = id.GetValue(),
            Name = name.GetValue(),
            Items = items.SelectMany(value => value.GetInt32()),
            Order = order.GetValue()
        };
    }
}
