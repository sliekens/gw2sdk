using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Skins;
using Newtonsoft.Json;

namespace GW2SDK.Features.Skins
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(WeaponSkinDiscriminatorOptions))]
    public class WeaponSkin : Skin
    {
        public DamageType DamageType { get; set; }
    }
}