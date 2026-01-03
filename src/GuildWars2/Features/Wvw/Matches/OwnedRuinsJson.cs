using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class OwnedRuinsJson
{
    public static OwnedRuins GetOwnedRuins(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember owner = "owner";
        RequiredMember lastFlipped = "last_flipped";
        RequiredMember pointsTick = "points_tick";
        RequiredMember pointsCapture = "points_capture";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Ruins"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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

        return new OwnedRuins
        {
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Owner = owner.Map(static (in value) => value.GetEnum<TeamColor>()),
            LastFlipped = lastFlipped.Map(static (in value) => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Map(static (in value) => value.GetInt32()),
            PointsCapture = pointsCapture.Map(static (in value) => value.GetInt32())
        };
    }
}
