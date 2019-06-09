using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
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