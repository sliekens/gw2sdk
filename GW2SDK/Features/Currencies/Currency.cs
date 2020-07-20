using System.Diagnostics;
using GW2SDK.Annotations;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    [DataTransferObject]
    [DebuggerDisplay("{Name,nq}")]
    public sealed class Currency
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public int Order { get; set; }

        public string Icon { get; set; } = "";
    }
}
