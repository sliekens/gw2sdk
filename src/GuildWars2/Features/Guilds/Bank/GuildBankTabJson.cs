using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Guilds.Bank;

internal static class GuildBankTabJson
{
    public static GuildBankTab GetGuildBankTab(this in JsonElement json)
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember size = "size";
        RequiredMember coins = "coins";
        OptionalMember note = "note";
        RequiredMember inventory = "inventory";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildBankTab
        {
            UpgradeId = upgradeId.Map(static (in value) => value.GetInt32()),
            Size = size.Map(static (in value) => value.GetInt32()),
            Coins = coins.Map(static (in value) => value.GetInt32()),
            Note = note.Map(static (in value) => value.GetString()) ?? "",
            Inventory = inventory.Map(static (in values) =>
                values.GetList(static (in value) => value.GetGuildBankSlot())
            )
        };
    }
}
