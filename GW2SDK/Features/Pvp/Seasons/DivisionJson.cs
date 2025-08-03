﻿using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class DivisionJson
{
    public static Division GetDivision(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember flags = "flags";
        RequiredMember largeIcon = "large_icon";
        RequiredMember smallIcon = "small_icon";
        RequiredMember pipIcon = "pip_icon";
        RequiredMember tiers = "tiers";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (largeIcon.Match(member))
            {
                largeIcon = member;
            }
            else if (smallIcon.Match(member))
            {
                smallIcon = member;
            }
            else if (pipIcon.Match(member))
            {
                pipIcon = member;
            }
            else if (tiers.Match(member))
            {
                tiers = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var largeIconString = largeIcon.Map(static (in JsonElement value) => value.GetStringRequired());
        var smallIconString = smallIcon.Map(static (in JsonElement value) => value.GetStringRequired());
        var pipIconString = pipIcon.Map(static (in JsonElement value) => value.GetStringRequired());
#pragma warning disable CS0618
        return new Division
        {
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Flags = flags.Map(static (in JsonElement values) => values.GetDivisionFlags()),
            LargeIconHref = largeIconString,
            LargeIconUrl = new Uri(largeIconString),
            SmallIconHref = smallIconString,
            SmallIconUrl = new Uri(smallIconString),
            PipIconHref = pipIconString,
            PipIconUrl = new Uri(pipIconString),
            Tiers = tiers.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetDivisionTier())
            )
        };
#pragma warning restore CS0618
    }
}
