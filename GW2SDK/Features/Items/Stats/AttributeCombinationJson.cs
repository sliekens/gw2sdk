﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items.Stats;

internal static class AttributeCombinationJson
{
    public static AttributeCombination GetAttributeCombination(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember attributes = "attributes";

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
            else if (attributes.Match(member))
            {
                attributes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AttributeCombination
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Attributes = attributes.Map(
                values => values.GetList(value => value.GetAttribute(missingMemberBehavior))
            )
        };
    }
}
