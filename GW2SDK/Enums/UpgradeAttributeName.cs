using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum UpgradeAttributeName
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(UpgradeAttributeName)
        AgonyResistance = 1,

        BoonDuration,

        ConditionDamage,

        ConditionDuration,

        CritDamage,

        Healing,

        Power,

        Precision,

        Toughness,

        Vitality
    }
}
