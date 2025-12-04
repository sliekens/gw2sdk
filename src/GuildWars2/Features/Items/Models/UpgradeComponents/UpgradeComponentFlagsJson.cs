using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal static class UpgradeComponentFlagsJson
{
    public static UpgradeComponentFlags GetUpgradeComponentFlags(this in JsonElement json)
    {
        bool heavyArmor = false;
        bool mediumArmor = false;
        bool lightArmor = false;
        bool axe = false;
        bool dagger = false;
        bool focus = false;
        bool greatsword = false;
        bool hammer = false;
        bool harpoon = false;
        bool longbow = false;
        bool mace = false;
        bool pistol = false;
        bool rifle = false;
        bool scepter = false;
        bool shield = false;
        bool shortbow = false;
        bool speargun = false;
        bool staff = false;
        bool sword = false;
        bool torch = false;
        bool trident = false;
        bool trinket = false;
        bool warhorn = false;
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
