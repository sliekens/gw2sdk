using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class CoinsRewardJson
{
    public static CoinsReward GetCoinsReward(this in JsonElement json)
    {
        RequiredMember coins = "count";
        foreach (JsonProperty member in json.EnumerateObject())
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

        return new CoinsReward { Coins = coins.Map(static (in value) => value.GetInt32()) };
    }
}
