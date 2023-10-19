﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class DivisionJson
{
    public static Division GetDivision(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember flags = new("flags");
        RequiredMember largeIcon = new("large_icon");
        RequiredMember smallIcon = new("small_icon");
        RequiredMember pipIcon = new("pip_icon");
        RequiredMember tiers = new("tiers");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(largeIcon.Name))
            {
                largeIcon.Value = member.Value;
            }
            else if (member.NameEquals(smallIcon.Name))
            {
                smallIcon.Value = member.Value;
            }
            else if (member.NameEquals(pipIcon.Name))
            {
                pipIcon.Value = member.Value;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Division
        {
            Name = name.Select(value => value.GetStringRequired()),
            Flags = flags.SelectMany(value => value.GetEnum<DivisionFlag>(missingMemberBehavior)),
            LargeIcon = largeIcon.Select(value => value.GetStringRequired()),
            SmallIcon = smallIcon.Select(value => value.GetStringRequired()),
            PipIcon = pipIcon.Select(value => value.GetStringRequired()),
            Tiers = tiers.SelectMany(value => value.GetDivisionTier(missingMemberBehavior))
        };
    }
}
