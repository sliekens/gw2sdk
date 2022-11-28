using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public static class ItemUpgradeJson
{
    public static ItemUpgrade GetItemUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<UpgradeType> upgrade = new("upgrade");
        RequiredMember<int> itemId = new("item_id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(upgrade.Name))
            {
                upgrade.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ItemUpgrade
        {
            Upgrade = upgrade.GetValue(missingMemberBehavior),
            ItemId = itemId.GetValue()
        };
    }
}
