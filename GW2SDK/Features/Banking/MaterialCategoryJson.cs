﻿using System.Text.Json;
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
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember items = "items";
        RequiredMember order = "order";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(items.Name))
            {
                items = member;
            }
            else if (member.NameEquals(order.Name))
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
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetStringRequired()),
            Items = items.SelectMany(value => value.GetInt32()),
            Order = order.Select(value => value.GetInt32())
        };
    }
}
