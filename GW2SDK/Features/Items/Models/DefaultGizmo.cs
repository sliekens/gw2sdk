using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record DefaultGizmo : Gizmo
    {
        public IReadOnlyCollection<int>? VendorIds { get; init; }
        
        public int? GuildUpgradeId { get; init; }
    }
}
