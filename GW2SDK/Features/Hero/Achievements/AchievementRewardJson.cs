using System.Text.Json;
using GuildWars2.Hero.Achievements.Rewards;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementRewardJson
{
    public static AchievementReward GetAchievementReward(
        this JsonElement json
    )
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Coins":
                    return json.GetCoinsReward();
                case "Item":
                    return json.GetItemReward();
                case "Mastery":
                    return json.GetMasteryPointReward();
                case "Title":
                    return json.GetTitleReward();
            }
        }

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var member in json.EnumerateObject())
        {
            if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementReward();
    }
}
