﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Legends;

[PublicAPI]
public static class LegendJson
{
    public static Legend GetLegend(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember code = new("code");
        RequiredMember swap = new("swap");
        RequiredMember heal = new("heal");
        RequiredMember elite = new("elite");
        RequiredMember utilities = new("utilities");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(swap.Name))
            {
                swap = member;
            }
            else if (member.NameEquals(code.Name))
            {
                code = member;
            }
            else if (member.NameEquals(heal.Name))
            {
                heal = member;
            }
            else if (member.NameEquals(elite.Name))
            {
                elite = member;
            }
            else if (member.NameEquals(utilities.Name))
            {
                utilities = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Legend
        {
            Id = id.Select(value => value.GetStringRequired()),
            Code = code.Select(value => value.GetInt32()),
            Swap = swap.Select(value => value.GetInt32()),
            Heal = heal.Select(value => value.GetInt32()),
            Elite = elite.Select(value => value.GetInt32()),
            Utilities = utilities.SelectMany(value => value.GetInt32())
        };
    }
}
