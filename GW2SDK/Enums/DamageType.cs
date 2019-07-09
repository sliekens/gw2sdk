using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum DamageType
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(DamageType)
        Choking = 1,

        Fire,

        Ice,

        Lightning,

        Physical
    }
}