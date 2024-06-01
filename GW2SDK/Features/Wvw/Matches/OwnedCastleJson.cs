using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Matches;

internal static class OwnedCastleJson
{
    public static OwnedCastle GetOwnedCastle(this JsonElement json)
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
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Castle"))
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
            else if (claimedBy.Match(member))
            {
                claimedBy = member;
            }
            else if (claimedAt.Match(member))
            {
                claimedAt = member;
            }
            else if (yaksDelivered.Match(member))
            {
                yaksDelivered = member;
            }
            else if (guildUpgrades.Match(member))
            {
                guildUpgrades = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new OwnedCastle
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Owner = owner.Map(static value => value.GetEnum<TeamColor>()),
            LastFlipped = lastFlipped.Map(static value => value.GetDateTimeOffset()),
            PointsTick = pointsTick.Map(static value => value.GetInt32()),
            PointsCapture = pointsCapture.Map(static value => value.GetInt32()),
            ClaimedBy = claimedBy.Map(static value => value.GetString()) ?? "",
            ClaimedAt = claimedAt.Map(static value => value.GetDateTimeOffset()),
            YaksDelivered = yaksDelivered.Map(static value => value.GetInt32()),
            GuildUpgrades =
                guildUpgrades.Map(static values => values.GetList(static value => value.GetInt32()))
                ?? Empty.ListOfInt32
        };
    }
}
