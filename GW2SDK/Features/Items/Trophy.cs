using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public sealed class Trophy : Item
    {
        public int Level { get; set; }
    }
}