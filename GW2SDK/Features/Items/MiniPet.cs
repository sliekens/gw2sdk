using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed class Minipet : Item
    {
        public int Level { get; set; }

        public int MinipetId { get; set; }
    }
}