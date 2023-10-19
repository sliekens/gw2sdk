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
        RequiredMember upgradeId = new("upgrade_id");
        RequiredMember size = new("size");
        RequiredMember coins = new("coins");
        OptionalMember note = new("note");
        RequiredMember inventory = new("inventory");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(upgradeId.Name))
            {
                upgradeId.Value = member.Value;
            }
            else if (member.NameEquals(size.Name))
            {
                size.Value = member.Value;
            }
            else if (member.NameEquals(coins.Name))
            {
                coins.Value = member.Value;
            }
            else if (member.NameEquals(note.Name))
            {
                note.Value = member.Value;
            }
            else if (member.NameEquals(inventory.Name))
            {
                inventory.Value = member.Value;
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
            Inventory = inventory.SelectMany(value => value.GetGuildBankSlot(missingMemberBehavior))
        };
    }
}
