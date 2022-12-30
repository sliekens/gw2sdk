using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
public static class CastleJson
{
    public static Castle GetCastle(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> id = new("id");
        RequiredMember<TeamColor> owner = new("owner");
        RequiredMember<DateTimeOffset> lastFlipped = new("last_flipped");
        RequiredMember<int> pointsTick = new("points_tick");
        RequiredMember<int> pointsCapture = new("points_capture");
        OptionalMember<string> claimedBy = new("claimed_by");
        NullableMember<DateTimeOffset> claimedAt = new("claimed_at");
        RequiredMember<int> yaksDelivered = new("yaks_delivered");
        RequiredMember<int> guildUpgrades = new("guild_upgrades");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Castle"))
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
            else if (member.NameEquals(claimedBy.Name))
            {
                claimedBy.Value = member.Value;
            }
            else if (member.NameEquals(claimedAt.Name))
            {
                claimedAt.Value = member.Value;
            }
            else if (member.NameEquals(yaksDelivered.Name))
            {
                yaksDelivered.Value = member.Value;
            }
            else if (member.NameEquals(guildUpgrades.Name))
            {
                guildUpgrades.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Castle
        {
            Id = id.GetValue(),
            Owner = owner.GetValue(missingMemberBehavior),
            LastFlipped = lastFlipped.GetValue(),
            PointsTick = pointsTick.GetValue(),
            PointsCapture = pointsCapture.GetValue(),
            ClaimedBy = claimedBy.GetValueOrEmpty(),
            ClaimedAt = claimedAt.GetValue(),
            YaksDelivered = yaksDelivered.GetValue(),
            GuildUpgrades = guildUpgrades.GetValues()
        };
    }
}
