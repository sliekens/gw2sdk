using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Bank;

[PublicAPI]
public static class GuildBankTabJson
{
    public static GuildBankTab GetGuildBankTab(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember size = "size";
        RequiredMember coins = "coins";
        OptionalMember note = "note";
        RequiredMember inventory = "inventory";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(upgradeId.Name))
            {
                upgradeId = member;
            }
            else if (member.NameEquals(size.Name))
            {
                size = member;
            }
            else if (member.NameEquals(coins.Name))
            {
                coins = member;
            }
            else if (member.NameEquals(note.Name))
            {
                note = member;
            }
            else if (member.NameEquals(inventory.Name))
            {
                inventory = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildBankTab
        {
            UpgradeId = upgradeId.Select(value => value.GetInt32()),
            Size = size.Select(value => value.GetInt32()),
            Coins = coins.Select(value => value.GetInt32()),
            Note = note.Select(value => value.GetString()) ?? "",
            Inventory = inventory.Select(values => values.GetList(value => value.GetGuildBankSlot(missingMemberBehavior)))
        };
    }
}
