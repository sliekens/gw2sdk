using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Bag
    {
        public int Id { get; init; }

        public int Size { get; init; }

        public IEnumerable<InventorySlot?> Inventory { get; init; } = Array.Empty<InventorySlot>();
    }
}