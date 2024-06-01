using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class CoinsRewardJson
{
    public static CoinsReward GetCoinsReward(this JsonElement json)
    {
        RequiredMember coins = "count";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Coins"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (coins.Match(member))
            {
                coins = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CoinsReward { Coins = coins.Map(static value => value.GetInt32()) };
    }
}
