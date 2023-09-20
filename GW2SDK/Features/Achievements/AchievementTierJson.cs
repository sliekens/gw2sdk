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
        RequiredMember<int> count = new("count");
        RequiredMember<int> points = new("points");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(points.Name))
            {
                points.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementTier
        {
            Count = count.GetValue(),
            Points = points.GetValue()
        };
    }
}
