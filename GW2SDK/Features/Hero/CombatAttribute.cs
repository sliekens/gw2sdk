﻿using System.ComponentModel;

namespace GuildWars2.Hero;

/// <summary>The primary and secondary combat attributes which improve character (and pet) effectiveness in combat. Primary
/// attributes have a base value of 1000 at level 80, while secondary attributes have a base value of 0.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum CombatAttribute
{
    /// <summary>No attribute.</summary>
    None,

    /// <summary>Concentration increases outgoing boon duration, including boons applied to self. Secondary attribute.</summary>
    BoonDuration,

    /// <summary>Ferocity increases outgoing critical damage. Secondary attribute.</summary>
    CritDamage,

    /// <summary>Condition damage increases outgoing condition damage. Secondary attribute.</summary>
    ConditionDamage,

    /// <summary>Expertise increases outgoing condition duration. Secondary attribute.</summary>
    ConditionDuration,

    /// <summary>Healing power increases outgoing healing, including self heals. Secondary attribute.</summary>
    Healing,

    /// <summary>Precision increases critical chance. Primary attribute.</summary>
    Precision,

    /// <summary>Toughness increases armor. Primary attribute.</summary>
    Toughness,

    /// <summary>Power increases outgoing strike damage. Primary attribute.</summary>
    Power,

    /// <summary>Vitality increases health. Primary attribute.</summary>
    Vitality,

    /// <summary>Agony Resistance improves resistance against Agony; only relevant inside Fractals of the Mists. Special
    /// attribute.</summary>
    AgonyResistance
}
