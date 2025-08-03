using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementTierJson
{
    public static AchievementTier GetAchievementTier(this in JsonElement json)
    {
        RequiredMember count = "count";
        RequiredMember points = "points";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (count.Match(member))
            {
                count = member;
            }
            else if (points.Match(member))
            {
                points = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementTier
        {
            Count = count.Map(static (in JsonElement value) => value.GetInt32()),
            Points = points.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
