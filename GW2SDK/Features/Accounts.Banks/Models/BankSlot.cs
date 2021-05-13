﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record BankSlot
    {
        public int Id { get; init; }

        public int Count { get; init; }

        public int? Charges { get; init; }

        public int? Skin { get; init; }

        public int[]? Upgrades { get; init; }

        /// <summary>Indicates which upgrade slots are in use. (0-based)</summary>
        public int[]? UpgradeSlotIndices { get; init; }

        public int[]? Infusions { get; init; }

        public ItemBinding Binding { get; init; }

        /// <summary>The name of the character when the item is Soulbound.</summary>
        public string BoundTo { get; init; } = "";

        public SelectedStat? Stats { get; init; }
    }
}
