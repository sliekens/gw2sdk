using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.AstralRewards;

internal static class AstralRewardJson
{
    public static AstralReward GetAstralReward(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AstralReward
        {
            Id = id.Map(static value => value.GetInt32()),
            ItemId = itemId.Map(static value => value.GetInt32()),
            ItemCount = itemCount.Map(static value => value.GetInt32()),
            Kind = type.Map(static value => value.GetEnum<RewardKind>()),
            Cost = cost.Map(static value => value.GetInt32())
        };
    }
}
