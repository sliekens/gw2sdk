using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    public enum MasteryRegionName
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(MasteryRegionName)
        Tyria = 1,

        Maguuma,

        Desert
    }
}
