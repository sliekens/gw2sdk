using GW2SDK.Annotations;

namespace GW2SDK.Enums
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
