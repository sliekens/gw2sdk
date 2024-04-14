using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Bank;

internal static class GuildBankTabJson
{
    public static GuildBankTab GetGuildBankTab(
        this JsonElement json
    )
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember size = "size";
        RequiredMember coins = "coins";
        OptionalMember note = "note";
        RequiredMember inventory = "inventory";

        foreach (var member in json.EnumerateObject())
        {
            if (upgradeId.Match(member))
            {
                upgradeId = member;
            }
            else if (size.Match(member))
            {
                size = member;
            }
            else if (coins.Match(member))
            {
                coins = member;
            }
            else if (note.Match(member))
            {
                note = member;
            }
            else if (inventory.Match(member))
            {
                inventory = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildBankTab
        {
            UpgradeId = upgradeId.Map(static value => value.GetInt32()),
            Size = size.Map(static value => value.GetInt32()),
            Coins = coins.Map(static value => value.GetInt32()),
            Note = note.Map(static value => value.GetString()) ?? "",
            Inventory = inventory.Map(static values => values.GetList(static value => value.GetGuildBankSlot())
            )
        };
    }
}
