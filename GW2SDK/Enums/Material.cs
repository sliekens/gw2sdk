using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum Material
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(Material)
        Cloth = 1,

        Leather,

        Metal
    }
}
