using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(UpgradeComponentDiscriminatorOptions))]
    public class UpgradeComponent : Item
    {
        public int Level { get; set; }

        [NotNull]
        public UpgradeComponentFlag[] UpgradeComponentFlags { get; set; }

        [CanBeNull]
        public InfusionSlotFlag[] InfusionUpgradeFlags { get; set; }
        
        [NotNull]
        public InfixUpgrade InfixUpgrade { get; set; }

        [CanBeNull]
        public string[] Bonuses { get; set; }

        [CanBeNull]
        public string Suffix { get; set; }
    }
}
