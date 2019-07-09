using GW2SDK.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlotInfo
    {
        [NotNull]
        [ItemCanBeNull]
        public DyeSlot[] Default { get; set; }

        [NotNull]
        public DyeSlotOverrideInfo Overrides { get; set; }
    }
}
