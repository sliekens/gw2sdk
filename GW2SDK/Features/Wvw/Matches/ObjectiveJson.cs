﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class ObjectiveJson
{
    public static Objective GetObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Camp":
                return json.GetCamp(missingMemberBehavior);
            case "Castle":
                return json.GetCastle(missingMemberBehavior);
            case "Keep":
                return json.GetKeep(missingMemberBehavior);
            case "Mercenary":
                return json.GetMercenary(missingMemberBehavior);
            case "Ruins":
                return json.GetRuins(missingMemberBehavior);
            case "Spawn":
                return json.GetSpawn(missingMemberBehavior);
            case "Tower":
                return json.GetTower(missingMemberBehavior);
        }

        RequiredMember id = new("id");
        RequiredMember owner = new("owner");
        RequiredMember lastFlipped = new("last_flipped");
        RequiredMember pointsTick = new("points_tick");
        RequiredMember pointsCapture = new("points_capture");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(owner.Name))
            {
                owner.Value = member.Value;
            }
            else if (member.NameEquals(lastFlipped.Name))
            {
                lastFlipped.Value = member.Value;
            }
            else if (member.NameEquals(pointsTick.Name))
            {
                pointsTick.Value = member.Value;
            }
            else if (member.NameEquals(pointsCapture.Name))
            {
                pointsCapture.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Id = id.Select(value => value.GetStringRequired()),
            Owner = owner.Select(value => value.GetEnum<TeamColor>(missingMemberBehavior)),
            LastFlipped = lastFlipped.Select(value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Select(value => value.GetInt32()),
            PointsCapture = pointsCapture.Select(value => value.GetInt32())
        };
    }
}
