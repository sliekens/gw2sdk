using System.Text.Json;
using GuildWars2.Hero.Masteries;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class MasteryPointRewardJson
{
    public static MasteryPointReward GetMasteryPointReward(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember region = "region";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Mastery"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (region.Match(member))
            {
                region = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPointReward
        {
            Id = id.Map(value => value.GetInt32()),
            Region = region.Map(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior))
        };
    }
}
