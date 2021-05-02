using GW2SDK.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    public record WeaponSkin : Skin
    {
        public DamageType DamageType { get; init; }
    }
}
