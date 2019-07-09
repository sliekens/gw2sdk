using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Skins.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(WeaponSkinDiscriminatorOptions))]
    public class WeaponSkin : Skin
    {
        public DamageType DamageType { get; set; }
    }
}