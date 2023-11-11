using System.Text.Json;
using GuildWars2.Hero.Achievements.Rewards;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementRewardJson
{
    public static AchievementReward GetAchievementReward(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Coins":
                return json.GetCoinsReward(missingMemberBehavior);
            case "Item":
                return json.GetItemReward(missingMemberBehavior);
            case "Mastery":
                return json.GetMasteryPointReward(missingMemberBehavior);
            case "Title":
                return json.GetTitleReward(missingMemberBehavior);
        }

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var member in json.EnumerateObject())
        {
            if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementReward();
    }
}
