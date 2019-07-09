using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;

namespace GW2SDK.Worlds
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class World
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public WorldPopulation Population { get; set; }
    }
}
