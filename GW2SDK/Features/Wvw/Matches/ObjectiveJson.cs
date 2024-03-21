using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class ObjectiveJson
{
    public static Objective GetObjective(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
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
        }

        RequiredMember id = "id";
        RequiredMember owner = "owner";
        RequiredMember lastFlipped = "last_flipped";
        RequiredMember pointsTick = "points_tick";
        RequiredMember pointsCapture = "points_capture";

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
            else if (id.Match(member))
            {
                id = member;
            }
            else if (owner.Match(member))
            {
                owner = member;
            }
            else if (lastFlipped.Match(member))
            {
                lastFlipped = member;
            }
            else if (pointsTick.Match(member))
            {
                pointsTick = member;
            }
            else if (pointsCapture.Match(member))
            {
                pointsCapture = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Objective
        {
            Id = id.Map(value => value.GetStringRequired()),
            Owner = owner.Map(value => value.GetEnum<TeamColor>(missingMemberBehavior)),
            LastFlipped = lastFlipped.Map(value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Map(value => value.GetInt32()),
            PointsCapture = pointsCapture.Map(value => value.GetInt32())
        };
    }
}
