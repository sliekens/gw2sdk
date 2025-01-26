using System.Text.Json;
using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class ItemFlagsJson
{
    public static ItemFlags GetItemFlags(this JsonElement json)
    {
        var accountBindOnUse = false;
        var accountBound = false;
        var attuned = false;
        var bulkConsume = false;
        var deleteWarning = false;
        var hideSuffix = false;
        var infused = false;
        var monsterOnly = false;
        var noMysticForge = false;
        var noSalvage = false;
        var noSell = false;
        var notUpgradeable = false;
        var noUnderwater = false;
        var soulbindOnAcquire = false;
        var soulBindOnUse = false;
        var tonic = false;
        var unique = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("AccountBindOnUse"))
            {
                accountBindOnUse = true;
            }
            else if (entry.ValueEquals("AccountBound"))
            {
                accountBound = true;
            }
            else if (entry.ValueEquals("Attuned"))
            {
                attuned = true;
            }
            else if (entry.ValueEquals("BulkConsume"))
            {
                bulkConsume = true;
            }
            else if (entry.ValueEquals("DeleteWarning"))
            {
                deleteWarning = true;
            }
            else if (entry.ValueEquals("HideSuffix"))
            {
                hideSuffix = true;
            }
            else if (entry.ValueEquals("Infused"))
            {
                infused = true;
            }
            else if (entry.ValueEquals("MonsterOnly"))
            {
                monsterOnly = true;
            }
            else if (entry.ValueEquals("NoMysticForge"))
            {
                noMysticForge = true;
            }
            else if (entry.ValueEquals("NoSalvage"))
            {
                noSalvage = true;
            }
            else if (entry.ValueEquals("NoSell"))
            {
                noSell = true;
            }
            else if (entry.ValueEquals("NotUpgradeable"))
            {
                notUpgradeable = true;
            }
            else if (entry.ValueEquals("NoUnderwater"))
            {
                noUnderwater = true;
            }
            else if (entry.ValueEquals("SoulbindOnAcquire"))
            {
                soulbindOnAcquire = true;
            }
            else if (entry.ValueEquals("SoulBindOnUse"))
            {
                soulBindOnUse = true;
            }
            else if (entry.ValueEquals("Tonic"))
            {
                tonic = true;
            }
            else if (entry.ValueEquals("Unique"))
            {
                unique = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new ItemFlags
        {
            AccountBindOnUse = accountBindOnUse,
            AccountBound = accountBound,
            Attuned = attuned,
            BulkConsume = bulkConsume,
            DeleteWarning = deleteWarning,
            HideSuffix = hideSuffix,
            Infused = infused,
            MonsterOnly = monsterOnly,
            NoMysticForge = noMysticForge,
            NoSalvage = noSalvage,
            NoSell = noSell,
            NotUpgradeable = notUpgradeable,
            NoUnderwater = noUnderwater,
            SoulbindOnUse = soulBindOnUse,
            Soulbound = soulbindOnAcquire,
            Tonic = tonic,
            Unique = unique,
            Other = others
        };
    }
}
