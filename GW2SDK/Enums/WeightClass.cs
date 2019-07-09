using GW2SDK.Annotations;

namespace GW2SDK.Enums
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
