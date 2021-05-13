using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record DyeUnlocker : Unlocker
    {
        public int ColorId { get; init; }
    }
}
