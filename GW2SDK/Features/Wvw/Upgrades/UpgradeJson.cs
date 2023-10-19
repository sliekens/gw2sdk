﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Upgrades;

[PublicAPI]
public static class UpgradeJson
{
    public static Upgrade GetUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember description = "description";
        RequiredMember icon = "icon";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(description.Name))
            {
                description = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Upgrade
        {
            Name = name.Select(value => value.GetStringRequired()),
            Description = description.Select(value => value.GetStringRequired()),
            IconHref = icon.Select(value => value.GetStringRequired())
        };
    }
}
