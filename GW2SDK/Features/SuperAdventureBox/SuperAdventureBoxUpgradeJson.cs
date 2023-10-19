﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
public static class SuperAdventureBoxUpgradeJson
{
    public static SuperAdventureBoxUpgrade GetSuperAdventureBoxUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        OptionalMember name = "name";

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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SuperAdventureBoxUpgrade
        {
            Id = id.Select(value => value.GetInt32()),
            Name = name.Select(value => value.GetString()) ?? ""
        };
    }
}
