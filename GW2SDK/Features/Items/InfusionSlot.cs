using GW2SDK.Annotations;

namespace GW2SDK.Items
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
