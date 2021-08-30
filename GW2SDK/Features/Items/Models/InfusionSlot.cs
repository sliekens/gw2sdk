using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record InfusionSlot
    {
        public InfusionSlotFlag[] Flags { get; init; } = Array.Empty<InfusionSlotFlag>();

        public int? ItemId { get; init; }
    }
}
