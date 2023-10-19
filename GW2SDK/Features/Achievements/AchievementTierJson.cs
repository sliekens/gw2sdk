using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class AchievementTierJson
{
    public static AchievementTier GetAchievementTier(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember count = "count";
        RequiredMember points = "points";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(count.Name))
            {
                count = member;
            }
            else if (member.NameEquals(points.Name))
            {
                points = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementTier
        {
            Count = count.Select(value => value.GetInt32()),
            Points = points.Select(value => value.GetInt32())
        };
    }
}
