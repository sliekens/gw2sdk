using System.Diagnostics;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Worlds
{
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class World
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Population Population { get; set; }
    }
}
