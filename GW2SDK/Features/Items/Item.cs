using System.Diagnostics;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Items;
using Newtonsoft.Json;

namespace GW2SDK.Features.Items
{
    [DebuggerDisplay("{Name,nq}")]
    [Inheritable]
    [DataTransferObject]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(ItemDiscriminatorOptions))]
    public class Item
    {
        public int Id { get; set; }

        /// <remarks>Name can be an empty string but not null.</remarks>
        [NotNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Description { get; set; }

        public ItemRarity Rarity { get; set; }

        public int VendorValue { get; set; }

        /// <remarks>GameTypes can be an empty array but not null.</remarks>
        public GameType[] GameTypes { get; set; }

        /// <remarks>Flags can be an empty array but not null.</remarks>
        public ItemFlag[] Flags { get; set; }

        /// <remarks>Restrictions can be an empty array but not null.</remarks>
        public ItemRestriction[] Restrictions { get; set; }

        [NotNull]
        public string ChatLink { get; set; }

        [CanBeNull]
        public string Icon { get; set; }

        [CanBeNull]
        public ItemUpgrade[] UpgradesFrom { get; set; }

        [CanBeNull]
        public ItemUpgrade[] UpgradesInto { get; set; }
    }
}
