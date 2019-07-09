using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class DyeUnlocker : Unlocker
    {
        public int ColorId { get; set; }
    }
}
