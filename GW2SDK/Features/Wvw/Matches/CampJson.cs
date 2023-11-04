using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class CampJson
{
    public static Camp GetCamp(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember owner = "owner";
        RequiredMember lastFlipped = "last_flipped";
        RequiredMember pointsTick = "points_tick";
        RequiredMember pointsCapture = "points_capture";
        OptionalMember claimedBy = "claimed_by";
        NullableMember claimedAt = "claimed_at";
        OptionalMember yaksDelivered = "yaks_delivered";
        OptionalMember guildUpgrades = "guild_upgrades";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Camp"))
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
            else if (member.Name == claimedBy.Name)
            {
                claimedBy = member;
            }
            else if (member.Name == claimedAt.Name)
            {
                claimedAt = member;
            }
            else if (member.Name == yaksDelivered.Name)
            {
                yaksDelivered = member;
            }
            else if (member.Name == guildUpgrades.Name)
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
            Id = id.Map(value => value.GetStringRequired()),
            Owner = owner.Map(value => value.GetEnum<TeamColor>(missingMemberBehavior)),
            LastFlipped = lastFlipped.Map(value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Map(value => value.GetInt32()),
            PointsCapture = pointsCapture.Map(value => value.GetInt32()),
            ClaimedBy = claimedBy.Map(value => value.GetString()) ?? "",
            ClaimedAt = claimedAt.Map(value => value.GetDateTimeOffset()),
            YaksDelivered = yaksDelivered.Map(value => value.GetInt32()),
            GuildUpgrades = guildUpgrades.Map(values => values.GetList(value => value.GetInt32()))
                ?? Empty.ListOfInt32
        };
    }
}
