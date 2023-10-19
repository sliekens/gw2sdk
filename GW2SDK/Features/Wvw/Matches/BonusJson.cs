﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class BonusJson
{
    public static Bonus GetBonus(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember type = new("type");
        RequiredMember owner = new("owner");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(type.Name))
            {
                type.Value = member.Value;
            }
            else if (member.NameEquals(owner.Name))
            {
                owner.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Bonus
        {
            Kind = type.Select(value => value.GetEnum<BonusKind>(missingMemberBehavior)),
            Owner = owner.Select(value => value.GetEnum<TeamColor>(missingMemberBehavior))
        };
    }
}
