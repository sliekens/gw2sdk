using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(WeaponDiscriminatorOptions))]
    public class Weapon : Equipment
    {
        public int DefaultSkin { get; set; }

        public DamageType DamageType { get; set; }

        public int MinPower { get; set; }

        public int MaxPower { get; set; }

        public int Defense { get; set; }
    }
}
