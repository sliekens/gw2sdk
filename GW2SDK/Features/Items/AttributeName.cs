namespace GW2SDK.Features.Items
{
    public enum AttributeName
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(AttributeName)
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