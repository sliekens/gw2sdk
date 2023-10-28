using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Bank;

internal static class GuildBankTabJson
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
            if (member.Name == upgradeId.Name)
            {
                upgradeId = member;
            }
            else if (member.Name == size.Name)
            {
                size = member;
            }
            else if (member.Name == coins.Name)
            {
                coins = member;
            }
            else if (member.Name == note.Name)
            {
                note = member;
            }
            else if (member.Name == inventory.Name)
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
            UpgradeId = upgradeId.Map(value => value.GetInt32()),
            Size = size.Map(value => value.GetInt32()),
            Coins = coins.Map(value => value.GetInt32()),
            Note = note.Map(value => value.GetString()) ?? "",
            Inventory = inventory.Map(
                values => values.GetList(value => value.GetGuildBankSlot(missingMemberBehavior))
            )
        };
    }
}
