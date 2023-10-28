﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class RuinsJson
{
    public static Ruins GetRuins(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember owner = "owner";
        RequiredMember lastFlipped = "last_flipped";
        RequiredMember pointsTick = "points_tick";
        RequiredMember pointsCapture = "points_capture";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Ruins"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == owner.Name)
            {
                owner = member;
            }
            else if (member.Name == lastFlipped.Name)
            {
                lastFlipped = member;
            }
            else if (member.Name == pointsTick.Name)
            {
                pointsTick = member;
            }
            else if (member.Name == pointsCapture.Name)
            {
                pointsCapture = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Ruins
        {
            Id = id.Map(value => value.GetStringRequired()),
            Owner = owner.Map(value => value.GetEnum<TeamColor>(missingMemberBehavior)),
            LastFlipped = lastFlipped.Map(value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Map(value => value.GetInt32()),
            PointsCapture = pointsCapture.Map(value => value.GetInt32())
        };
    }
}
