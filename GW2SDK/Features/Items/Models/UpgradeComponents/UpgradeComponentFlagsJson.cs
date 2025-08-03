using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UpgradeComponentFlagsJson
{
    public static UpgradeComponentFlags GetUpgradeComponentFlags(this in JsonElement json)
    {
        var heavyArmor = false;
        var mediumArmor = false;
        var lightArmor = false;
        var axe = false;
        var dagger = false;
        var focus = false;
        var greatsword = false;
        var hammer = false;
        var harpoon = false;
        var longbow = false;
        var mace = false;
        var pistol = false;
        var rifle = false;
        var scepter = false;
        var shield = false;
        var shortbow = false;
        var speargun = false;
        var staff = false;
        var sword = false;
        var torch = false;
        var trident = false;
        var trinket = false;
        var warhorn = false;
        ValueList<string> others = [];
        foreach (JsonElement entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Axe"))
            {
                axe = true;
            }
            else if (entry.ValueEquals("Dagger"))
            {
                dagger = true;
            }
            else if (entry.ValueEquals("Focus"))
            {
                focus = true;
            }
            else if (entry.ValueEquals("Greatsword"))
            {
                greatsword = true;
            }
            else if (entry.ValueEquals("Hammer"))
            {
                hammer = true;
            }
            else if (entry.ValueEquals("Harpoon"))
            {
                harpoon = true;
            }
            else if (entry.ValueEquals("LongBow"))
            {
                longbow = true;
            }
            else if (entry.ValueEquals("Mace"))
            {
                mace = true;
            }
            else if (entry.ValueEquals("Pistol"))
            {
                pistol = true;
            }
            else if (entry.ValueEquals("Rifle"))
            {
                rifle = true;
            }
            else if (entry.ValueEquals("Scepter"))
            {
                scepter = true;
            }
            else if (entry.ValueEquals("Shield"))
            {
                shield = true;
            }
            else if (entry.ValueEquals("ShortBow"))
            {
                shortbow = true;
            }
            else if (entry.ValueEquals("Speargun"))
            {
                speargun = true;
            }
            else if (entry.ValueEquals("Staff"))
            {
                staff = true;
            }
            else if (entry.ValueEquals("Sword"))
            {
                sword = true;
            }
            else if (entry.ValueEquals("Torch"))
            {
                torch = true;
            }
            else if (entry.ValueEquals("Trident"))
            {
                trident = true;
            }
            else if (entry.ValueEquals("Trinket"))
            {
                trinket = true;
            }
            else if (entry.ValueEquals("Warhorn"))
            {
                warhorn = true;
            }
            else if (entry.ValueEquals("HeavyArmor"))
            {
                heavyArmor = true;
            }
            else if (entry.ValueEquals("MediumArmor"))
            {
                mediumArmor = true;
            }
            else if (entry.ValueEquals("LightArmor"))
            {
                lightArmor = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new UpgradeComponentFlags
        {
            HeavyArmor = heavyArmor,
            MediumArmor = mediumArmor,
            LightArmor = lightArmor,
            Axe = axe,
            Dagger = dagger,
            Focus = focus,
            Greatsword = greatsword,
            Hammer = hammer,
            Spear = harpoon,
            LongBow = longbow,
            Mace = mace,
            Pistol = pistol,
            Rifle = rifle,
            Scepter = scepter,
            Shield = shield,
            ShortBow = shortbow,
            HarpoonGun = speargun,
            Staff = staff,
            Sword = sword,
            Torch = torch,
            Trident = trident,
            Trinket = trinket,
            Warhorn = warhorn,
            Other = others
        };
    }
}
