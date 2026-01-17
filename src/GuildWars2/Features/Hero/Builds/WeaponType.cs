using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>The weapon types.</summary>
[DefaultValue(Unknown)]
[JsonConverter(typeof(WeaponTypeJsonConverter))]
public enum WeaponType
{
    /// <summary>No specific weapon type or unknown weapon type.</summary>
    Unknown,

    /// <summary>No weapon type.</summary>
    None,

    /// <summary>The dagger is a one-handed weapon that may also be wielded in the main hand and off-hand. Daggers are known
    /// for their fast and aggressive attacks.</summary>
    Dagger,

    /// <summary>The focus is an off-hand only weapon. Foci are primarily used by scholar professions and are characterized by
    /// having powerful defensive skills and utility.</summary>
    Focus,

    /// <summary>The staff is a two-handed weapon that is well known for its long range and wide array of area of effect skills
    /// that can support allies or damage and control enemies. Most skills on a staff have 1200 range, however Revenants and
    /// Daredevils instead use the staff in close combat.</summary>
    Staff,

    /// <summary>The scepter is a mid-ranged main-hand weapon that excels in damage and control.</summary>
    Scepter,

    /// <summary>The sword is a one-handed weapon that may be wielded in the main hand and off-hand. Swords balance positioning
    /// and avoiding attacks with inflicting damage, making them versatile for any profession to use.</summary>
    Sword,

    /// <summary>The trident is a long range magical aquatic weapon.</summary>
    Trident,

    /// <summary>The pistol is a one-handed weapon that may be wielded in the main hand and off-hand. Pistols are characterized
    /// by their mid-range rapid projectile attacks.</summary>
    Pistol,

    /// <summary>The rifle is a two-handed weapon that is known for its high single-target damage output and long-range
    /// projectiles.</summary>
    Rifle,

    /// <summary>The shield is an off-hand only weapon. Shields typically grant defensive skills to mitigate incoming damage or
    /// inflict control effects. Unlike all other weapons, shields also increase defense of the wielder.</summary>
    Shield,

    /// <summary>The harpoon gun is a long range mechanical aquatic weapon.</summary>
    HarpoonGun,

    /// <summary>The greatsword is a two-handed weapon. Greatswords are typically characterized by their relatively high damage
    /// and slow attacks.</summary>
    Greatsword,

    /// <summary>The mace is a one-handed weapon that may be wielded in the main hand and off-hand. Maces are used by all
    /// soldier professions and specialize in damage, control, or support based on the profession that wields it.</summary>
    Mace,

    /// <summary>The torch is an off-hand only weapon. Torches always grant at least one skill that burns foes. The torch also
    /// provides a source of light in darker areas.</summary>
    Torch,

    /// <summary>The hammer is a two-handed weapon. Hammers are known for their wide variety of control skills and breadth of
    /// their attacks. They are melee weapons except Revenant who uses them as a long range weapon.</summary>
    Hammer,

    /// <summary>The spear is a close-quarters melee aquatic weapon.</summary>
    Spear,

    /// <summary>The axe is a one-handed weapon that may be wielded in the main hand and off-hand. Their skills typically deal
    /// great damage and control movement.</summary>
    Axe,

    /// <summary>The warhorn is an off-hand only weapon. Warhorns are known for their support skills and always have a skill
    /// that gives the caster swiftness.</summary>
    Warhorn,

    /// <summary>The short bow is a mid-range, two-handed weapon with a high attack rate and ability to apply conditions.</summary>
    ShortBow,

    /// <summary>The longbow is a two-handed weapon that is known for its long-range projectiles, wide area of effect abilities
    /// and strong control skills to keep foes away.</summary>
    Longbow,

    /// <summary>A large bundle (two-handed), which is a weapon that replaces your skills when equipped.</summary>
    LargeBundle,

    /// <summary>Empty handed.</summary>
    Nothing
}
