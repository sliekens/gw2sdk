using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record InfusionSlot
    {
        public IReadOnlyCollection<InfusionSlotFlag> Flags { get; init; } = Array.Empty<InfusionSlotFlag>();

        public int? ItemId { get; init; }
    }
}
