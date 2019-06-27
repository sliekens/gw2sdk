using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
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