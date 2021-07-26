using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DyeSlot
    {
        public int ColorId { get; init; }

        public Material Material { get; init; }
    }
}
