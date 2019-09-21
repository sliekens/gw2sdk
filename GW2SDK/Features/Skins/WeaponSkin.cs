using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    public class WeaponSkin : Skin
    {
        [JsonProperty(Required = Required.Always)]
        public DamageType DamageType { get; set; }
    }
}
