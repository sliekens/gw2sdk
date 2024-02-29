using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.AstralRewards;

internal static class PurchasedAstralRewardJson
{
    public static PurchasedAstralReward GetPurchasedAstralReward(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember itemCount = "item_count";
        RequiredMember type = "type";
        RequiredMember cost = "cost";
        NullableMember purchased = "purchased";
        NullableMember purchaseLimit = "purchase_limit";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == itemId.Name)
            {
                itemId = member;
            }
            else if (member.Name == itemCount.Name)
            {
                itemCount = member;
            }
            else if (member.Name == type.Name)
            {
                type = member;
            }
            else if (member.Name == cost.Name)
            {
                cost = member;
            }
            else if (member.Name == purchased.Name)
            {
                purchased = member;
            }
            else if (member.Name == purchaseLimit.Name)
            {
                purchaseLimit = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PurchasedAstralReward
        {
            Id = id.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32()),
            ItemCount = itemCount.Map(value => value.GetInt32()),
            Kind = type.Map(value => value.GetEnum<RewardKind>(missingMemberBehavior)),
            Cost = cost.Map(value => value.GetInt32()),
            Purchased = purchased.Map(value => value.GetInt32()),
            PurchaseLimit = purchaseLimit.Map(value => value.GetInt32())
        };
    }
}
