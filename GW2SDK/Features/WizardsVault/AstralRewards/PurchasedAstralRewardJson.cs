using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.WizardsVault.AstralRewards;

internal static class PurchasedAstralRewardJson
{
    public static PurchasedAstralReward GetPurchasedAstralReward(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember itemCount = "item_count";
        RequiredMember type = "type";
        RequiredMember cost = "cost";
        NullableMember purchased = "purchased";
        NullableMember purchaseLimit = "purchase_limit";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (purchased.Match(member))
            {
                purchased = member;
            }
            else if (purchaseLimit.Match(member))
            {
                purchaseLimit = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new PurchasedAstralReward
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32()),
            ItemCount = itemCount.Map(static (in JsonElement value) => value.GetInt32()),
            Kind = type.Map(static (in JsonElement value) => value.GetEnum<RewardKind>()),
            Cost = cost.Map(static (in JsonElement value) => value.GetInt32()),
            Purchased = purchased.Map(static (in JsonElement value) => value.GetInt32()),
            PurchaseLimit = purchaseLimit.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}
