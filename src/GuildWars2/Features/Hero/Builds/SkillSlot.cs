using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds;

// The underscores are part of the API data so keeping them is necessary.

#pragma warning disable CA1707 // Identifiers should not contain underscores

/// <summary>The skill slots for abilities which can be activated by the player.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(SkillSlotJsonConverter))]
public enum SkillSlot
{
    /// <summary>No specific skill slot or unknown skill slot.</summary>
    None,

    /// <summary>The first weapon skill. Granted by two-handed weapons or one-handed weapons in the main hand.</summary>
    Weapon_1,

    /// <summary>The second weapon skill. Granted by two-handed weapons or one-handed weapons in the main hand.</summary>
    Weapon_2,

    /// <summary>The third weapon skill. Granted by two-handed weapons or one-handed weapons in the main hand.</summary>
    Weapon_3,

    /// <summary>The fourth weapon skill. Granted by two-handed weapons or one-handed weapons in the off-hand.</summary>
    Weapon_4,

    /// <summary>The fifth weapon skill. Granted by two-handed weapons or one-handed weapons in the off-hand.</summary>
    Weapon_5,

    /// <summary>The heal skill. Each character has 1 selectable heal skill slot.</summary>
    Heal,

    /// <summary>The utility skills. Each character has 3 selectable utility skill slots.</summary>
    Utility,

    /// <summary>The elite skill. Each character has 1 selectable elite skill slot.</summary>
    Elite,

    /// <summary>The first profession skill. Each profession has a different mechanic that occupy profession skill slots.</summary>
    Profession_1,

    /// <summary>The second profession skill. Each profession has a different mechanic that occupy profession skill slots.</summary>
    Profession_2,

    /// <summary>The third profession skill. Each profession has a different mechanic that occupy profession skill slots.</summary>
    Profession_3,

    /// <summary>The fourth profession skill. Each profession has a different mechanic that occupy profession skill slots.</summary>
    Profession_4,

    /// <summary>The fifth profession skill. Each profession has a different mechanic that occupy profession skill slots.</summary>
    Profession_5,

    /// <summary>The first downstate skill. Each profession has a different skill for this slot. It is also used for
    /// transformation skills.</summary>
    Downed_1,

    /// <summary>The second downstate skill. Each profession has a different skill for this slot. It is also used for
    /// transformation skills.</summary>
    Downed_2,

    /// <summary>The third downstate skill. Each profession has a different skill for this slot. It is also used for
    /// transformation skills.</summary>
    Downed_3,

    /// <summary>The fourth downstate skill. Each profession has a different downstate skillbar. It is also used for
    /// transformation skills.</summary>
    Downed_4,

    /// <summary>The pet skills. Only used by rangers.</summary>
    Pet,

    /// <summary>Toolbelt skills. Only used by engineers.</summary>
    Toolbelt,

    /// <summary>Placeholder slot for the 'Locked' skill. It is only used to replace other skills while transformed.</summary>
    Transform_1
}
