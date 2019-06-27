using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlot
    {
        public int ColorId { get; set; }

        // TODO: material should be an enum
        public string Material { get; set; }
    }
}
