using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Wardrobe;

internal static class SkinFlagsJson
{
    public static SkinFlags GetSkinFlags(this in JsonElement json)
    {
        var hideIfLocked = false;
        var noCost = false;
        var overrideRarity = false;
        var showInWardrobe = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("HideIfLocked"))
            {
                hideIfLocked = true;
            }
            else if (entry.ValueEquals("NoCost"))
            {
                noCost = true;
            }
            else if (entry.ValueEquals("OverrideRarity"))
            {
                overrideRarity = true;
            }
            else if (entry.ValueEquals("ShowInWardrobe"))
            {
                showInWardrobe = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new SkinFlags
        {
            HideIfLocked = hideIfLocked,
            NoCost = noCost,
            OverrideRarity = overrideRarity,
            ShowInWardrobe = showInWardrobe,
            Other = others
        };
    }
}
