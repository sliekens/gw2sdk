using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class MercenaryJson
{
    public static Mercenary GetMercenary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember owner = new("owner");
        RequiredMember lastFlipped = new("last_flipped");
        RequiredMember pointsTick = new("points_tick");
        RequiredMember pointsCapture = new("points_capture");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Mercenary"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
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

        return new Mercenary
        {
            Id = id.Select(value => value.GetStringRequired()),
            Owner = owner.Select(value => value.GetEnum<TeamColor>(missingMemberBehavior)),
            LastFlipped = lastFlipped.Select(value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Select(value => value.GetInt32()),
            PointsCapture = pointsCapture.Select(value => value.GetInt32())
        };
    }
}
