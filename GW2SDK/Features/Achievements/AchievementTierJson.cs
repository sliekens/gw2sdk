using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

internal static class AchievementTierJson
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
            if (member.Name == count.Name)
            {
                count = member;
            }
            else if (member.Name == points.Name)
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
            Count = count.Map(value => value.GetInt32()),
            Points = points.Map(value => value.GetInt32())
        };
    }
}
