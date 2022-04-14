using System;
using System.Text.Json;

using GW2SDK.Banking.Models;
using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Banking.Json;

[PublicAPI]
public static class MaterialCategoryReader
{
    public static MaterialCategory Read(JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<int> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> items = new("items");
        RequiredMember<int> order = new("order");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(name.Name))
            {
                name = name.From(member.Value);
            }
            else if (member.NameEquals(items.Name))
            {
                items = items.From(member.Value);
            }
            else if (member.NameEquals(order.Name))
            {
                order = order.From(member.Value);
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