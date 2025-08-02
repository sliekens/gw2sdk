using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

/// <summary>Skills with similar behaviors are grouped into one of these categories. Many traits affect all skills in a
/// certain category.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(SkillCategoryNameJsonConverter))]
public enum SkillCategoryName
{
    /// <summary>No specific skill category or unknown skill category.</summary>
    None,

    /// <summary> Elementalist utility skills. Non-elemental magical energy sources of critical damage.</summary>
    Arcane,

    /// <summary>Warrior utility skills. Buffs for allies that can be picked up, moved and replanted.</summary>
    Banner,

    /// <summary>Warrior profession mechanic. Powerful attacks that expend all built-up adrenaline, whose effects scale
    /// depending on its amount.</summary>
    Burst,

    /// <summary>Elementalist, Untamed and Deadeye utility skills. Magical tricks that enhance one's survivability.</summary>
    Cantrip,

    /// <summary>Druid profession mechanic. Special weapon skills focused on area of effect healing and support available in a
    /// druid's astral form.</summary>
    CelestialAvatar,

    /// <summary>Mesmer profession mechanic. Temporary allies with the caster's name and appearance to mislead the enemy.</summary>
    Clone,

    /// <summary>Elementalist utility skills. Potent elemental bundle summons for yourself and allies.</summary>
    Conjure,

    /// <summary>Guardian utility skills. Area of effect skills that aid allies or punish enemies.</summary>
    Consecration,

    /// <summary>Necromancer utility skills. Inflict condition upon the caster to cause powerful effects.</summary>
    Corruption,

    /// <summary>Thied and Mirage utility skills. Defensive or evasive maneuvers.</summary>
    Deception,

    /// <summary>Thief. Third weapon skill which is determined by the combination of main-hand and off-hand weapon.</summary>
    DualWield,

    /// <summary>Engineer and Harbinger utility skills. Random effects to support yourself, your team or to harass your
    /// enemies.</summary>
    Elixir,

    /// <summary>Engineer utility skills. Skills involving mechanical gadgetry.</summary>
    Gadget,

    /// <summary>Mesmer utility skills. Ethereal fields that have special, manipulative effects.</summary>
    Glamour,

    /// <summary>Elementalist and Druid utility skills. Grants a buff or causes an effect based on the current state of your
    /// profession mechanic.</summary>
    Glyph,

    /// <summary>Engineer utility skills. Replace weapon skills with specialized tools or weapons.</summary>
    Kit,

    /// <summary>Revenant utility skills. Skills themed around the assassin Shiro Tagachi, focusing on high mobility and
    /// damage.</summary>
    LegendaryAssassin,

    /// <summary>Revenant utility skills. Skills themed around the demon Mallyx the Unyielding, focusing on manipulation of
    /// conditions.</summary>
    LegendaryDemon,

    /// <summary>Herald utility skills. Skills themed around the dragon Glint, focusing on support through various boons, whose
    /// channeling can be released to devastate an area.</summary>
    LegendaryDragon,

    /// <summary>Revenant utility skills. Skills themed around the dwarf king Jalis Ironhammer, focusing on defense.</summary>
    LegendaryDwarf,

    /// <summary>Mesmer utility skills. Skills that mislead your opponents.</summary>
    Manipulation,

    /// <summary>Mesmer and Guardian utility skills. Chargeable skills that can be instantly released several times for various
    /// effects.</summary>
    Mantra,

    /// <summary>Necromancer utility skills. Ground-targeted spells which are triggered by foes moving into the area.</summary>
    Mark,

    /// <summary>Guardian and Spellbreaker utility skills. Beneficial effects, often with instant activation.</summary>
    Meditation,

    /// <summary>Necromancer utility skills. Undead allies that attack foes and can be consumed by their master for various
    /// effects.</summary>
    Minion,

    /// <summary>Tempest profession mechanic. Powerful channeled abilities tied to the attunements.</summary>
    Overload,

    /// <summary>Mesmer utility skills. Temporary phantom versions of the caster which attack enemies with different abilities.</summary>
    Phantasm,

    /// <summary>Warrior, Willbender and Daredevil utility skills. Controlling of enemies by using brute force.</summary>
    Physical,

    /// <summary>Berserker profession mechanic. Berserking versions of burst skills.</summary>
    PrimalBurst,

    /// <summary>Berserker profession mechanic (partially). Feats of strength that grant adrenaline to the berserker.</summary>
    Rage,

    /// <summary>Warrior, Guardian, Tempest and Reaper utility skills. Large area of effect skills with fast or instant
    /// activation which support you and your allies, or harm your foes.</summary>
    Shout,

    /// <summary>Warrior, Guardian, Ranger, Thief, Elementalist, Mesmer, Necromancer and Mechanist utility skills. Passive
    /// benefits which can be activated for another effect instead.</summary>
    Signet,

    /// <summary>Necromancer utility skills. Spells involving otherworldly spectral energy.</summary>
    Spectral,

    /// <summary>Ranger utility skills. Nature spirits, which influence the battlefield around their location.</summary>
    Spirit,

    /// <summary>Guardian utility skills. Temporary spirit weapon summons, which automatically fight your target and which can
    /// be commanded for a special effect.</summary>
    SpiritWeapon,

    /// <summary>Warrior, Weaver and Soulbeast utility skills. Short-term defensive or offensive enhancements.</summary>
    Stance,

    /// <summary>Thief. Enhanced version of the first weapon skill while stealthed.</summary>
    StealthAttack,

    /// <summary>Ranger utility skills. Effects that boost your damage or survivability.</summary>
    Survival,

    /// <summary>Guardian utility skills. Area of effect attack, which hinders enemies and aids allies for a short duration.</summary>
    Symbol,

    /// <summary>Spells that change the caster's appearance and weapon skills.</summary>
    Transform,

    /// <summary>Ranger and Dragonhunter utility skills. Hidden effects placed on the ground, that are triggered by enemies
    /// walking over them.</summary>
    Trap,

    /// <summary>Thief utility skills. Cunning maneuvers to control enemies, enhance damage and escape from harm.</summary>
    Trick,

    /// <summary>Engineer utility skills. Deployed stationary devices to help defend and control an area.</summary>
    Turret,

    /// <summary>Thief utility skills. Apply a creature's venom to give your weapon attacks special effects.</summary>
    Venom,

    /// <summary>Guardian profession mechanic. Passive buffs occasionally granting a beneficial effect to the guardian, which
    /// can be sacrificed for a more potent effect for you and your allies.</summary>
    Virtue,

    /// <summary>Guardian utility skills. Areas across the ground that prevent enemies from crossing them.</summary>
    Ward,

    /// <summary>Necromancer, Engineer, Specter and Chronomancer utility skills. Persistent spells affecting the surrounding
    /// area by harming foes and supporting allies.</summary>
    Well
}
