using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class InfusionSlot
    {
        [NotNull]
        public InfusionSlotFlag[] Flags { get; set; }

        public int? ItemId { get; set; }
    }
}
