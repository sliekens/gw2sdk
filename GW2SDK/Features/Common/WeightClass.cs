using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
    public enum WeightClass
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(WeightClass)
        Clothing = 1,

        Light,

        Medium,

        Heavy
    }
}
