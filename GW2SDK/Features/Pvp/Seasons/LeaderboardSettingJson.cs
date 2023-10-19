using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
public static class LeaderboardSettingJson
{
    public static LeaderboardSetting GetLeaderboardSetting(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = new("name");
        RequiredMember scoring = new("scoring");
        RequiredMember tiers = new("tiers");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals("duration") && member.Value.ValueKind == JsonValueKind.Null)
            {
                // Ignore, seems to be always null
            }
            else if (member.NameEquals(scoring.Name))
            {
                scoring.Value = member.Value;
            }
            else if (member.NameEquals(tiers.Name))
            {
                tiers.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeaderboardSetting
        {
            Name = name.Select(value => value.GetStringRequired()),
            ScoringId = scoring.Select(value => value.GetStringRequired()),
            Tiers = tiers.SelectMany(value => value.GetLeaderboardTier(missingMemberBehavior))
        };
    }
}
