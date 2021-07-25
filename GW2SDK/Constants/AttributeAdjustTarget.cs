using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    [DefaultValue(None)]
    public enum AttributeAdjustTarget
    {
        None,

        BoonDuration,

        CritDamage,

        ConditionDamage,

        ConditionDuration,

        Healing,

        Precision,

        Toughness,

        Power,

        Vitality
    }
}
