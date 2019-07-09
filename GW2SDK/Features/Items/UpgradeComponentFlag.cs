using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public enum UpgradeComponentFlag
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(UpgradeComponentFlag)
        Axe = 1,

        Dagger,

        Focus,

        Greatsword,

        Hammer,

        Harpoon,

        HeavyArmor,

        LightArmor,

        LongBow,

        Mace,

        MediumArmor,

        Pistol,

        Rifle,

        Scepter,

        Shield,

        ShortBow,

        Speargun,

        Staff,

        Sword,

        Torch,

        Trident,

        Trinket,

        Warhorn
    }
}