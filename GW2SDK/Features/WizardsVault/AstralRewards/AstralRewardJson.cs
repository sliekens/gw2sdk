using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.AstralRewards;

internal static class AstralRewardJson
{
    public static AstralReward GetAstralReward(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember itemCount = "item_count";
        RequiredMember type = "type";
        RequiredMember cost = "cost";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (itemCount.Match(member))
            {
                itemCount = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (cost.Match(member))
            {
                cost = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AstralReward
        {
            Id = id.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32()),
            ItemCount = itemCount.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<RewardKind>()),
            Cost = cost.Map(value => value.GetInt32())
        };
    }
}
