using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record DefaultGizmo : Gizmo
    {
        public int[]? VendorIds { get; init; }
        
        public int? GuildUpgradeId { get; init; }
    }
}
