using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record InfusionSlot
    {
        public InfusionSlotFlag[] Flags { get; init; } = new InfusionSlotFlag[0];

        public int? ItemId { get; init; }
    }
}
