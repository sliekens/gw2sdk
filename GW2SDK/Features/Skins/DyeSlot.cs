using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlot
    {
        public int ColorId { get; set; }

        public Material Material { get; set; }
    }
}
