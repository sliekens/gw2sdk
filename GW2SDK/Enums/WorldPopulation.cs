using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum WorldPopulation
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(WorldPopulation)
        Low = 1,

        Medium,

        High,

        VeryHigh,

        Full
    }
}
