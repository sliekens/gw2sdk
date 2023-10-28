using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class ItemUpgradeJson
{
    public static ItemUpgrade GetItemUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgrade = "upgrade";
        RequiredMember itemId = "item_id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == upgrade.Name)
            {
                upgrade = member;
            }
            else if (member.Name == itemId.Name)
            {
                itemId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemUpgrade
        {
            Upgrade = upgrade.Map(value => value.GetEnum<UpgradeType>(missingMemberBehavior)),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
