using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class OwnedObjectiveJson
{
    public static OwnedObjective GetOwnedObjective(this JsonElement json)
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Camp":
                    return json.GetOwnedCamp();
                case "Castle":
                    return json.GetOwnedCastle();
                case "Keep":
                    return json.GetOwnedKeep();
                case "Mercenary":
                    return json.GetOwnedMercenary();
                case "Ruins":
                    return json.GetOwnedRuins();
                case "Spawn":
                    return json.GetOwnedSpawn();
                case "Tower":
                    return json.GetOwnedTower();
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
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new OwnedObjective
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Owner = owner.Map(static value => value.GetEnum<TeamColor>()),
            LastFlipped = lastFlipped.Map(static value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Map(static value => value.GetInt32()),
            PointsCapture = pointsCapture.Map(static value => value.GetInt32())
        };
    }
}
