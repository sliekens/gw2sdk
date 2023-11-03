using System.ComponentModel;

namespace GuildWars2;

[PublicAPI]
[DefaultValue(None)]
public enum AttributeAdjustmentTarget
{
    /// <summary>No effect.</summary>
    None,

    /// <summary>Concentration.</summary>
    BoonDuration,

    /// <summary>Ferocity.</summary>
    CritDamage,

    /// <summary>Condition damage.</summary>
    ConditionDamage,

    /// <summary>Expertise</summary>
    ConditionDuration,

    /// <summary>Healing.</summary>
    Healing,

    /// <summary>Precision.</summary>
    Precision,

    /// <summary>Toughness.</summary>
    Toughness,

    /// <summary>Power.</summary>
    Power,

    /// <summary>Vitality.</summary>
    Vitality
}
