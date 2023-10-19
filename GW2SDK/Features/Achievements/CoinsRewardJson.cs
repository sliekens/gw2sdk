using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class CoinsRewardJson
{
    public static CoinsReward GetCoinsReward(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember coins = new("count");
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
            else if (member.NameEquals(coins.Name))
            {
                coins.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CoinsReward { Coins = coins.Select(value => value.GetInt32()) };
    }
}
