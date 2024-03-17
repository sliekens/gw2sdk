using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class CoinsRewardJson
{
    public static CoinsReward GetCoinsReward(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coins = "count";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Coins"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (coins.Match(member))
            {
                coins = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CoinsReward { Coins = coins.Map(value => value.GetInt32()) };
    }
}
