using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Skins
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