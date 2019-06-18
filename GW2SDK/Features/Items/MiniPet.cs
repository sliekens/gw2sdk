using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public sealed class Minipet : Item
    {
        public int Level { get; set; }

        public int MinipetId { get; set; }
    }
}