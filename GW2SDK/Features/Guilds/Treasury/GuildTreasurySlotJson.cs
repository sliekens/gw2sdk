﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Treasury;

[PublicAPI]
public static class GuildTreasurySlotJson
{
    public static GuildTreasurySlot GetGuildTreasurySlot(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> itemId = new("item_id");
        RequiredMember<int> count = new("count");
        RequiredMember<CountNeededForUpgrade> countNeededForUpgrade = new("needed_by");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(countNeededForUpgrade.Name))
            {
                countNeededForUpgrade.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildTreasurySlot
        {
            ItemId = itemId.GetValue(),
            Count = count.GetValue(),
            CountNeededForUpgrades = countNeededForUpgrade.SelectMany(
                value => value.GetCountNeededForUpgrade(missingMemberBehavior)
            )
        };
    }
}
