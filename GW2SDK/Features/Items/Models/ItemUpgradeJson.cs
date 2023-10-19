using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Items;

[PublicAPI]
public static class ItemUpgradeJson
{
    public static ItemUpgrade GetItemUpgrade(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgrade = new("upgrade");
        RequiredMember itemId = new("item_id");
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
            Upgrade = upgrade.Select(value => value.GetEnum<UpgradeType>(missingMemberBehavior)),
            ItemId = itemId.Select(value => value.GetInt32())
        };
    }
}
