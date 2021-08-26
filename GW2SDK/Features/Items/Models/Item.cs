using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    public record Item
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public string Description { get; init; } = "";
        
        public int Level { get; init; }
        
        public Rarity Rarity { get; init; }
        
        public Coin VendorValue { get; init; }
        
        /// <remarks>Can be empty.</remarks>
        public GameType[] GameTypes { get; init; } = new GameType[0];
        
        /// <remarks>Can be empty.</remarks>
        public ItemFlag[] Flags { get; init; } = new ItemFlag[0];
        
        /// <remarks>Can be empty.</remarks>
        public ItemRestriction[] Restrictions { get; init; } = new ItemRestriction[0];

        public string ChatLink { get; init; } = "";

        public string? Icon { get; init; }
    }
}
