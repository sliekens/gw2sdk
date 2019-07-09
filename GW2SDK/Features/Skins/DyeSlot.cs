using GW2SDK.Annotations;
using GW2SDK.Enums;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class DyeSlot
    {
        public int ColorId { get; set; }

        public Material Material { get; set; }
    }
}
