using GW2SDK.Annotations;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    public enum Population
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(Population)
        Low = 1,

        Medium,

        High,

        VeryHigh,

        Full
    }
}
