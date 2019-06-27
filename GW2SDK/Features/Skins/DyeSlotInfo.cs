using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Skins
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
