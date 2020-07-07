using System.ComponentModel;
using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DefaultValue(None)]
    public enum TraitTarget
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