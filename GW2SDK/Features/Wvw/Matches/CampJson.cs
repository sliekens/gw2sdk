using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class CampJson
{
    public static Camp GetCamp(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember owner = new("owner");
        RequiredMember lastFlipped = new("last_flipped");
        RequiredMember pointsTick = new("points_tick");
        RequiredMember pointsCapture = new("points_capture");
        OptionalMember claimedBy = new("claimed_by");
        NullableMember claimedAt = new("claimed_at");
        OptionalMember yaksDelivered = new("yaks_delivered");
        OptionalMember guildUpgrades = new("guild_upgrades");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Camp"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(owner.Name))
            {
                owner = member;
            }
            else if (member.NameEquals(lastFlipped.Name))
            {
                lastFlipped = member;
            }
            else if (member.NameEquals(pointsTick.Name))
            {
                pointsTick = member;
            }
            else if (member.NameEquals(pointsCapture.Name))
            {
                pointsCapture = member;
            }
            else if (member.NameEquals(claimedBy.Name))
            {
                claimedBy = member;
            }
            else if (member.NameEquals(claimedAt.Name))
            {
                claimedAt = member;
            }
            else if (member.NameEquals(yaksDelivered.Name))
            {
                yaksDelivered = member;
            }
            else if (member.NameEquals(guildUpgrades.Name))
            {
                guildUpgrades = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Camp
        {
            Id = id.Select(value => value.GetStringRequired()),
            Owner = owner.Select(value => value.GetEnum<TeamColor>(missingMemberBehavior)),
            LastFlipped = lastFlipped.Select(value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Select(value => value.GetInt32()),
            PointsCapture = pointsCapture.Select(value => value.GetInt32()),
            ClaimedBy = claimedBy.Select(value => value.GetString()) ?? "",
            ClaimedAt = claimedAt.Select(value => value.GetDateTimeOffset()),
            YaksDelivered = yaksDelivered.Select(value => value.GetInt32()),
            GuildUpgrades = guildUpgrades.SelectMany(value => value.GetInt32()) ?? Array.Empty<int>()
        };
    }
}
