using System.Diagnostics;
using GW2SDK.Annotations;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class World
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Population Population { get; set; }
    }
}
