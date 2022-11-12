using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skins;

[PublicAPI]
[Inheritable]
public record WeaponSkin : Skin
{
    public required DamageType DamageType { get; init; }
}
