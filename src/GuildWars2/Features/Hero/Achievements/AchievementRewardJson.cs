using System.Text.Json;

using GuildWars2.Hero.Achievements.Rewards;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementRewardJson
{
    public static AchievementReward GetAchievementReward(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
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
                default:
                    break;
            }
        }

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementReward();
    }
}
